using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.UI
{
    [DependsOn(typeof(TchivsAbpUIModule))]
    public class AbpUIMasaBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMasaBlazor();
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpUIMasaBlazorModule).Assembly;
            });
        }
    }
}