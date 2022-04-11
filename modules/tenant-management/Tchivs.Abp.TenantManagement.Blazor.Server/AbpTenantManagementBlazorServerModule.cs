using Tchivs.Abp.AspNetCore.Components.Server;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.Server
{
    [DependsOn(typeof(AbpTenantManagementBlazorModule) ,
        typeof(TchivsAbpAspNetCoreComponentsServerModule))]

    public class AbpTenantManagementBlazorServerModule:AbpModule
    {
      
    }
}