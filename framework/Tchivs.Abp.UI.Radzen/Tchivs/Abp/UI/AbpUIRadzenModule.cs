using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Radzen;
using Radzen.Blazor;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.UI
{
    [DependsOn(typeof(TchivsAbpUIModule))]
    public class AbpUIRadzenModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddScoped<DialogService>();
            context.Services.AddScoped<NotificationService>();
            context.Services.AddScoped<TooltipService>();
            context.Services.AddScoped<ContextMenuService>();
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpUIRadzenModule).Assembly;
            });
        }
    }
}