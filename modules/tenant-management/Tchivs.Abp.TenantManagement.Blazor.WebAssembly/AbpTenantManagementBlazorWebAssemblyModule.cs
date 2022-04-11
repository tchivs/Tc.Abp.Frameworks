using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Tchivs.Abp.TenantManagement.Blazor;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Tchivs.Abp.TenantManagement.Blazor.WebAssembly
{
 [DependsOn(typeof(AbpTenantManagementBlazorModule)  , 
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(TchivsAbpAspNetCoreComponentsWebAssemblyModule))]
    public class AbpTenantManagementBlazorWebAssemblyModule : AbpModule
    {
      
    }
}