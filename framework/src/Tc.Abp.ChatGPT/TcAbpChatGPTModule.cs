using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.Extensions.Options;
using Flurl.Http.Configuration;

namespace Tc.Abp.ChatGPT
{
    [DependsOn(typeof(Volo.Abp.Caching.AbpCachingModule))]
    public class TcAbpChatGPTModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            var configuration = context.Services.GetConfiguration();
            var sesion = configuration.GetSection("ChatGPT");
            if (sesion != null)
            {
                Configure<ChatGptOptions>(sesion);
            }
            context.Services.AddHttpClient<IChatGptClient, ChatGptClient>()
                .ConfigurePrimaryHttpMessageHandler((p) =>
                {
                    var cfg = p.GetRequiredService<IChatGptOptionsFactory>();
                    var proxy = cfg.GetOptions().Proxy;
                    var handler = new HttpClientHandler();
                    if (proxy != null)
                    {
                        handler.Proxy = new WebProxy(proxy);
                        handler.UseProxy = true;
                    }

                    return handler;
                });
            //context.Services.AddSingleton<IHistoryMessageStore,CacheHistoryMessageProvider>();
            context.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

        }
    }
}
