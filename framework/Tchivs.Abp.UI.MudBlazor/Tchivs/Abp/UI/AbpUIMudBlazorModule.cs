using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.UI
{
    [DependsOn(typeof(TchivsAbpUIModule))]
    public class AbpUIMudBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMudServices();
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpUIMudBlazorModule).Assembly;
            });
        }
    }
}