using System;
using System.Collections.Generic;
using System.Text;
using Tchivs.Abp.IdentityServer.Localization;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.IdentityServer
{
    [DependsOn(typeof(AbpIdentityServerDomainSharedModule))]
    public class TchivsAbpIdentityServerDomainSharedModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TchivsAbpIdentityServerDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<TchivsAbpIdentityServerResource>("en")
                    .AddBaseTypes(typeof(AbpIdentityServerResource))
                    .AddVirtualJson("/Localization/IdentityServer");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("IdentityServer", typeof(TchivsAbpIdentityServerResource));
            });
        }
    }
}
