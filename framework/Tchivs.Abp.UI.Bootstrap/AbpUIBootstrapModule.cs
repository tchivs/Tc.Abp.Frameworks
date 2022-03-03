using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.Bootstrap
{
    [DependsOn(
  
    typeof(AbpUIModule)
    )]
    public class AbpUIBootstrapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureBootstrapBlazor(context);
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpUIBootstrapModule).Assembly;
            });

        }

        private void ConfigureBootstrapBlazor(ServiceConfigurationContext context)
        {
            context.Services.AddBootstrapBlazor();
         
        }
    }
}
