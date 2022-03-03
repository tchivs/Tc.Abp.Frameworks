using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Tchivs.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Tchivs.Abp.Identity.Blazor.WebAssembly
{
    [DependsOn(typeof(TchivsAbpIdentityBlazorModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
       typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule)
        )]
    public class TchivsAbpIdentityBlazorWebAssemblyModule : AbpModule
    {
    }
}
