using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Tchivs.Abp.Identity.Blazor.WebAssembly
{
    [DependsOn(typeof(AbpIdentityBlazorModule),
       typeof(TchivsAbpAspNetCoreComponentsWebAssemblyModule),
       typeof(AbpIdentityHttpApiClientModule),
       typeof(AbpPermissionManagementHttpApiClientModule))]
    public class AbpIdentityBlazorWebAssemblyModule : AbpModule
    {
    }
}
