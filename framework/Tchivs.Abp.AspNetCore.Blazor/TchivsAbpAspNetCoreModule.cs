using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.Core;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Tchivs.Abp.AspNetCore.Blazor;
[DependsOn(
        typeof(TchivsAbpCoreModule),
typeof(AbpExceptionHandlingModule),
typeof(AbpAutoMapperModule),
typeof(AbpAspNetCoreComponentsWebModule),
typeof(AbpDddApplicationContractsModule),
typeof(AbpAuthorizationModule),
typeof(AbpUiNavigationModule))]
public class TchivsAbpAspNetCoreBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(AbpBlazorMessageLocalizerHelper<>));
        context.Services.AddBootstrapBlazor();

        ConfigureRouter(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(TchivsAbpAspNetCoreBlazorModule).Assembly);
        });
        // Configure<AbpRouterOptions>(options => { options.AppAssembly = typeof(TchivsAbpAspNetCoreBlazorModule).Assembly; });
    }
}