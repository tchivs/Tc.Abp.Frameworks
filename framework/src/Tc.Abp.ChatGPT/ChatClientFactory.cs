using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tc.Abp.ChatGPT.Exceptions;
using Tc.Abp.ChatGPT.Models;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.ChatGPT
{
    public enum ChatApiVersion
    {
        V1, V3
    }

    public interface IChatClient
    {
        Task<ChatGptResponse> AskAsync(Guid messageId, string message, CancellationToken cancellationToken = default);
        IAsyncEnumerable<ChatGptResponse> AskStreamAsync(Guid conversationId, string message, CancellationToken cancellationToken = default);
    }
    public class ChatClient : IChatClient
    {
        private IConversationManager conversation => this.lazyServiceProvider.LazyGetRequiredService<IConversationManager>();
        private ILogger logger => this.lazyServiceProvider.LazyGetRequiredService<ILogger<ChatClient>>();

        private readonly IAbpLazyServiceProvider lazyServiceProvider;

        private readonly IFlurlClient flurlClient;
        public ChatClient(IAbpLazyServiceProvider lazyServiceProvider, ChatGptOptions option, IFlurlClient flurlClient)
        {
            this.lazyServiceProvider = lazyServiceProvider;
            this.flurlClient = flurlClient;
            options = option;
            flurlClient.OnError(OnError);
            flurlClient.Settings.JsonSerializer = new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializerSettings()
            {
                DefaultValueHandling     = Newtonsoft.Json.DefaultValueHandling.Ignore,
                NullValueHandling        = Newtonsoft.Json.NullValueHandling.Ignore,
                ContractResolver         = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
            });
        }

        private void OnError(FlurlCall flurlCall)
        {
            logger.LogError(flurlCall.RequestBody);
            logger.LogError(flurlCall.Response.ToString());
            logger.LogError(flurlCall.Exception.Message,flurlCall.Exception);
        }

        public ChatGptOptions options;

        public async Task<ChatGptResponse> AskAsync(Guid conversationId, string message, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(message);

            // Ensures that conversationId isn't empty.
            if (conversationId == Guid.Empty)
            {
                conversationId = Guid.NewGuid();
            }

            var messages = await conversation.SetupAsync(conversationId, message);
            //var request = CreateRequest(messages, false, parameters, model);
            var request = CreateRequest(messages, false);
            var response =  await Completions().PostJsonAsync(request, cancellationToken).ReceiveJson<ChatGptResponse>();
            response!.ConversationId = conversationId;
            if (response.IsSuccessful)
            {
                // Adds the response message to the conversation cache.
                await conversation.UpdateHistory(conversationId, messages, response.Choices[0].Message);
            }
            else if (options.ThrowExceptionOnError)
            {
                throw new ChatGptException(response.Error!, System.Net.HttpStatusCode.OK);
            }

            return response;
        }
        private static readonly JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        public async IAsyncEnumerable<ChatGptResponse> AskStreamAsync(Guid conversationId, string message, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var messages = await conversation.SetupAsync(conversationId, message);
            var request = CreateRequest(messages, true);
            using var responseStream = await Completions().PostJsonAsync(request, cancellationToken).ReceiveStream();
            var contentBuilder = new StringBuilder();
            using var reader = new StreamReader(responseStream);
            while (!reader.EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
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
            // Adds the response message to the conversation cache.
            await conversation.UpdateHistory(conversationId, messages, new()
            {
                Role = ChatGptRoles.Assistant,
                Content = contentBuilder.ToString()
            });
        }
        private IFlurlRequest Completions()
        {
            return this.flurlClient.WithOAuthBearerToken(this.options.ApiKey).Request("v1/chat/completions");
        }

        private ChatGptRequest CreateRequest(List<ChatGptMessage> messages, bool stream, ChatGptParameters? parameters = null, string? model = null)
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
    public class ChatClientBuilder : ITransientDependency
    {
        private ChatGptOptions option => this.optionsFactory.GetOptions();

        public IAbpLazyServiceProvider? LazyServiceProvider { get; }

        private IChatGptOptionsFactory optionsFactory => this.LazyServiceProvider!.LazyGetRequiredService<IChatGptOptionsFactory>();

        private IFlurlClientFactory flurlClientFactory => this.LazyServiceProvider!.LazyGetRequiredService<IFlurlClientFactory>();

        public ChatClientBuilder(IAbpLazyServiceProvider abpLazyServiceProvider)
        {
            LazyServiceProvider = abpLazyServiceProvider;
        }
        public ChatClientBuilder Init()
        {
            if (!option.Proxy.IsNullOrEmpty())
            {
                ConfigureClient((x, o) =>
                {
                    x.Settings.HttpClientFactory = new ProxyHttpClientFactory(o);
                });
            }
            return this;
        }
        public ChatClientBuilder ConfigureClient(Action<IFlurlClient> configAction)
        {
            flurlClientFactory.ConfigureClient(this.option.BaseUrl, configAction);
            return this;
        }
        public ChatClientBuilder ConfigureClient(Action<IFlurlClient, ChatGptOptions> configAction)
        {
            flurlClientFactory.ConfigureClient(this.option.BaseUrl, x =>
            {
                configAction.Invoke(x, this.option);
            });
            return this;
        }
        public ChatClientBuilder ConfigureChatGpt(Action<ChatGptOptions> configAction)
        {
            configAction.Invoke(this.option);
            return this;
        }
        private IFlurlClient GetFlurlClient() =>
            flurlClientFactory.Get(this.option.BaseUrl);
        public IChatClient Build()
        {
            return new ChatClient(LazyServiceProvider!, option, this.GetFlurlClient());
        }
    }



}
