using Tchivs.Abp.AspNetCore.Components.WebAssembly.Bootstrap;
using Tchivs.Abp.TenantManagement.Blazor.Bootstrap;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.WebAssembly
{
 [DependsOn(typeof(AbpTenantManagementBlazorBootstrapModule), 
        typeof(WebAssemblyBootstrapModule),
        typeof(AbpTenantManagementBlazorWebAssemblyModule))]
    public class AbpTenantManagementBlazorWebAssemblyBootstrapModule : AbpModule
    {
      
    }
}