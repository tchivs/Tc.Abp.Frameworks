using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Tchivs.Abp.AspNetCore.Blazor.Localization;
using Tchivs.Abp.Core;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{

    [DependsOn(
        typeof(TchivsAbpCoreModule),
        typeof(BlazorLocalizationModule),
        typeof(AbpExceptionHandlingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreComponentsWebModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpUiNavigationModule))]
    public class TchivsAbpAspNetCoreBlazorAbstractionsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {     
            context.Services.AddSingleton(typeof(AbpBlazorMessageLocalizerHelper<>));
           
        }
    
    }
}