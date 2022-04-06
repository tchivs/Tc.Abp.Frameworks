using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Tchivs.Abp.TenantManagement.Blazor;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.TenantManagement.Blazor.WebAssembly
{
 [DependsOn(typeof(AbpTenantManagementBlazorModule)  ,  typeof(WebAssemblyThemingModule))]
    public class AbpTenantManagementBlazorWebAssemblyModule : AbpModule
    {
      
    }
}