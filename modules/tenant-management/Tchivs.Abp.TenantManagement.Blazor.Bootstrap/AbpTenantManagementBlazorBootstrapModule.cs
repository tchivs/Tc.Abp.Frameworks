using Microsoft.JSInterop;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.TenantManagement.Blazor.Bootstrap
{
  [DependsOn(typeof(AbpUIBootstrapModule),
        typeof(AbpTenantManagementBlazorModule))]

    public class AbpTenantManagementBlazorBootstrapModule:AbpModule 
    {
     
    }
}