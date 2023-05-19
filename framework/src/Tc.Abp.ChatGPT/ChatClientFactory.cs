using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.ChatGPT.Models;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.ChatGPT
{
    public enum ChatApiVersion
    {
        V1,V3
    }
    public record ChatClientOption
    {
       public ChatApiVersion ApiVersion { get; set; }=ChatApiVersion.V1;
      public  string BaseUrl { get; set; } = "https://api.openai.com";
        public string? ApiKey { get; set; }
    }
    public interface IChatClientFactory
    {
        public IChatClient CreateClient(ChatClientOption option,IFlurlClient flurlClient);
    }
    public interface IChatClient
    {
        Task<ChatGptResponse> AskAsync(Guid messageId,string message,CancellationToken cancellationToken);
    }
    public class ChatClient : IChatClient
    {
        private readonly IConversationManager conversation;
        private readonly ChatClientOption ChatClientOption;
        private readonly IFlurlClient flurlClient;
        public ChatClient(IAbpLazyServiceProvider lazyServiceProvider,IConversationManager conversation, ChatClientOption option, IFlurlClient  flurlClient )
        {
            this.conversation = conversation;
            this.ChatClientOption = option;
            this.flurlClient = flurlClient;
            options = new ChatGptOptions();
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

            throw new NotImplementedException();
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
    public class ChatClientBuilder
    {
        private readonly ChatClientOption option;
        private readonly IChatClientFactory chatClientFactory;
        private   IFlurlClientFactory flurlClientFactory;

        public ChatClientBuilder(ChatClientOption option, IChatClientFactory chatClientFactory, IFlurlClientFactory flurlClientFactory)
        {
            this.option = option;
            this.chatClientFactory = chatClientFactory;
            this.flurlClientFactory = flurlClientFactory;
        }

        public ChatClientBuilder ConfigureClient(Action<IFlurlClient> configAction)
        {
            this.flurlClientFactory = flurlClientFactory.ConfigureClient(this.option.BaseUrl, configAction);
            return this;
        }
        private IFlurlClient GetFlurlClient()=>
            flurlClientFactory.Get(this.option.BaseUrl);
        public IChatClient Build()
        {
            return chatClientFactory.CreateClient(option,this.GetFlurlClient());
        }
    }
    public class ChatClientFactory : IChatClientFactory, Volo.Abp.DependencyInjection.ISingletonDependency
    {
        private readonly IAbpLazyServiceProvider lazyServiceProvider;

        public ChatClientFactory(IAbpLazyServiceProvider lazyServiceProvider)
        {
            this.lazyServiceProvider = lazyServiceProvider;
        }
        public IChatClient CreateClient(ChatClientOption option, IFlurlClient flurlClient)
        {
            return new ChatClient(lazyServiceProvider,option, flurlClient);
        }
    }


}
