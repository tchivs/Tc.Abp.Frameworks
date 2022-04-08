using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.UI.Localization;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.UI
{
    [DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpAspNetCoreComponentsWebModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpUiNavigationModule),
     typeof(AbpExceptionHandlingModule),
    typeof(AbpAutoMapperModule)
    )]
    public class TchivsAbpUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton(typeof(AbpBlazorMessageLocalizerHelper<>));
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TchivsAbpUIModule>();
            });
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BlazorUIResource>("zh-Hans")
                    // .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/BlazorUI");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("BlazorUI", typeof(BlazorUIResource));
            });
        }
    }
}
