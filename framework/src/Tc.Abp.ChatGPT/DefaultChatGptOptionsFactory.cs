using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.ChatGPT
{
    public class DefaultChatGptOptionsFactory : IChatGptOptionsFactory, ISingletonDependency
    {
        private readonly IConfiguration configuration;
        private readonly ChatGptOptions options;

        public DefaultChatGptOptionsFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
            options = new ChatGptOptions();
            string sectionName = "ChatGPT";
            configuration.GetSection(sectionName).Bind(options);
        }
        public ChatGptOptions GetOptions()
        {
            return options;
        }
    }
}
