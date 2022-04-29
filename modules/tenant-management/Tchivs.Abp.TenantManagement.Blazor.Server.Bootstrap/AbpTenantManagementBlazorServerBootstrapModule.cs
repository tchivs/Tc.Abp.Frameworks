using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Bootstrap.Server;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.Server
{
    [DependsOn(typeof(AbpTenantManagementBlazorBootstrapModule),
        typeof(AbpTenantManagementBlazorServerModule),
        typeof(AbpUIBootstrapServerModule))]

    public class AbpTenantManagementBlazorServerBootstrapModule : AbpModule
    {
      
    }
}