using System.Net.Http.Json;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tc.Abp.ChatGPT.Exceptions;
using Tc.Abp.ChatGPT.Models;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tc.Abp.ChatGPT;
public class ConversationOption:IScopedDependency
{
    public TimeSpan MessageExpiration { get;set;}=TimeSpan.FromHours(1);
    public int MessageLimit { get; set; }
}
public class ChatGptClient : IChatGptClient, IScopedDependency
{

    private static readonly JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    private readonly IConversationManager conversation;

    public ICurrentUser CurrentUser { get; }
    public ChatGptOptions options;

    public HttpClient httpClient
    {
        get;
    }
    public ChatGptClient(HttpClient httpClient,  IChatGptOptionsFactory chatGptOptionsFactory, ICurrentUser CurrentUser,IConversationManager conversation)
    {
       
        this.httpClient = httpClient;
        options = chatGptOptionsFactory.GetOptions();
        this.CurrentUser = CurrentUser;
        this.conversation = conversation;
        options.User ??= CurrentUser.UserName;
        options.Organization ??= CurrentUser.TenantId?.ToString();

        this.httpClient.BaseAddress = new Uri(options.BaseUrl);
        this.httpClient.DefaultRequestHeaders.Authorization = new("Bearer", options.ApiKey);
        if (!string.IsNullOrWhiteSpace(options.Organization))
        {
            this.httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", options.Organization);
        }
    }

    
    private async Task<IList<ChatGptMessage>> CreateMessageList(Guid conversationId, string message)
    {
        // Checks whether a list of messages for the given conversationId already exists.
        var conversationHistory = await conversation.GetAsync(conversationId);

        List<ChatGptMessage> messages = conversationHistory is not null ? new(conversationHistory) : new();

        messages.Add(new()
        {
            Role = ChatGptRoles.User,
            Content = message
        });

        return messages;
    }
    public async Task<ChatGptResponse> AskAsync(Guid conversationId, string message, ChatGptParameters? parameters = null, string? model = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        // Ensures that conversationId isn't empty.
        if (conversationId == Guid.Empty)
        {
            conversationId = Guid.NewGuid();
        }

        var messages = await CreateMessageList(conversationId, message);
        var request = CreateRequest(messages, false, parameters, model);

        using var httpResponse = await httpClient.PostAsJsonAsync("chat/completions", request, jsonSerializerOptions, cancellationToken);
        var response = await httpResponse.Content.ReadFromJsonAsync<ChatGptResponse>(jsonSerializerOptions, cancellationToken: cancellationToken);
        response!.ConversationId = conversationId;

        if (response.IsSuccessful)
        {
            // Adds the response message to the conversation cache.
            await conversation.UpdateHistory(conversationId, messages, response.Choices[0].Message);
        }
        else if (options.ThrowExceptionOnError)
        {
            throw new ChatGptException(response.Error!, httpResponse.StatusCode);
        }

        return response;
    }
   
    public async IAsyncEnumerable<ChatGptResponse> AskStreamAsync(Guid conversationId, string message, ChatGptParameters? parameters = null, string? model = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        // Ensures that conversationId isn't empty.
        if (conversationId == Guid.Empty)
        {
            conversationId = Guid.NewGuid();
        }

        var messages = await CreateMessageList(conversationId, message);
        var request = CreateRequest(messages, true, parameters, model);

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
        {
            Content = new StringContent(JsonSerializer.Serialize(request, jsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        using var httpResponse = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        if (httpResponse.IsSuccessStatusCode)
        {
            var contentBuilder = new StringBuilder();

            using (var responseStream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken))
            {
                using var reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync() ?? string.Empty;
                    if (line.StartsWith("data: {"))
                    {
                        var json = line["data: ".Length..];
                        var response = JsonSerializer.Deserialize<ChatGptResponse>(json, jsonSerializerOptions);

                        var content = response!.Choices?[0].Delta?.Content;

                        if (!string.IsNullOrEmpty(content))
                        {
                            if (contentBuilder.Length == 0)
                            {
                                // If this is the first response, trims all the initial special characters.
                                content = content.TrimStart('\n');
                                response.Choices![0].Delta!.Content = content;
                            }

                            // Yields the response only if there is an actual content.
                            if (content != string.Empty)
                            {
                                contentBuilder.Append(content);

                                response.ConversationId = conversationId;
                                yield return response;
                            }
                        }
                    }
                    else if (line.StartsWith("data: [DONE]"))
                    {
                        break;
                    }
                }
            }

            // Adds the response message to the conversation cache.
            await conversation.UpdateHistory(conversationId, messages, new()
            {
                Role = ChatGptRoles.Assistant,
                Content = contentBuilder.ToString()
            });
        }
        else
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<ChatGptResponse>(cancellationToken: cancellationToken);

            if (options.ThrowExceptionOnError)
            {
                throw new ChatGptException(response!.Error!, httpResponse.StatusCode);
            }

            response!.ConversationId = conversationId;
            yield return response;
        }
    }

    

    private ChatGptRequest CreateRequest(IList<ChatGptMessage> messages, bool stream, ChatGptParameters? parameters = null, string? model = null)
        => new()
        {
            Model = model ?? options.DefaultModel,
            Messages = messages.ToArray(),
            Stream = stream,
            Temperature = parameters?.Temperature ?? options.DefaultParameters.Temperature,
            TopP = parameters?.TopP ?? options.DefaultParameters.TopP,
            Choices = parameters?.Choices ?? options.DefaultParameters.Choices,
            MaxTokens = parameters?.MaxTokens ?? options.DefaultParameters.MaxTokens,
            PresencePenalty = parameters?.PresencePenalty ?? options.DefaultParameters.PresencePenalty,
            FrequencyPenalty = parameters?.FrequencyPenalty ?? options.DefaultParameters.FrequencyPenalty,
            User = options.User,
        };

     
}
