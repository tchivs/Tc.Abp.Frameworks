using Tchivs.Abp.AspNetCore.Components.Server.Bootstrap;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.Server
{
    [DependsOn(typeof(AbpTenantManagementBlazorBootstrapModule),
        typeof(AbpTenantManagementBlazorServerModule),
        typeof(ServerBootstrapModule))]

    public class AbpTenantManagementBlazorServerBootstrapModule : AbpModule
    {
      
    }
}