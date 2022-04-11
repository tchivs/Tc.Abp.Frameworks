using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.FeatureManagement.Blazor;
using Tchivs.Abp.TenantManagement.Blazor.Navigation;
using Tchivs.Abp.UI;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Tchivs.Abp.TenantManagement.Blazor
{
    [DependsOn(typeof(TchivsAbpUIModule),
        typeof(AbpFeatureManagementBlazorModule),
        typeof(AbpTenantManagementApplicationContractsModule))]
    public class AbpTenantManagementBlazorModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpTenantManagementBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpTenantManagementBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
            });

         
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        TenantManagementModuleExtensionConsts.ModuleName,
                        TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                        createFormTypes: new[] { typeof(TenantCreateDto) },
                        editFormTypes: new[] { typeof(TenantUpdateDto) }
                    );
            });
        }
    }
}