using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Tchivs.Abp.TenantManagement.Blazor
{
    [DependsOn(typeof(AbpUIModule),
        typeof(AbpTenantManagementApplicationContractsModule))]
    public class AbpTenantManagementBlazorModule : AbpModule
    {

    }
}