using Microsoft.JSInterop;
using Tchivs.Abp.FeatureManagement.Blazor;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.TenantManagement.Blazor
{
    [DependsOn(typeof(AbpUIBootstrapModule),
          typeof(AbpFeatureManagementBlazorBootstrapModule),
          typeof(AbpTenantManagementBlazorModule))]

    public class AbpTenantManagementBlazorBootstrapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbpTenantManagementBlazorBootstrapModule).Assembly);
            });
        }

    }
}