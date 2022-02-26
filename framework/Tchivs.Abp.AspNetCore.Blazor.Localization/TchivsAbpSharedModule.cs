
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.AspNetCore.Blazor.Localization
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class BlazorLocalizationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BlazorLocalizationModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BlazorUIResource>("zh-Hans")
                   //.AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/BlazorUI");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("BlazorUI", typeof(BlazorUIResource));
            });
        }
    }
}