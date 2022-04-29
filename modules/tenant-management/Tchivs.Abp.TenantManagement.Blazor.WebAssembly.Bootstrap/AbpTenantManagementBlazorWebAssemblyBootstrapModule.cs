using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Bootstrap.WebAssembly;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.WebAssembly
{
 [DependsOn(typeof(AbpTenantManagementBlazorBootstrapModule), 
        typeof(AbpUIBootstrapWebAssemblyModule),
        typeof(AbpTenantManagementBlazorWebAssemblyModule))]
    public class AbpTenantManagementBlazorWebAssemblyBootstrapModule : AbpModule
    {
      
    }
}