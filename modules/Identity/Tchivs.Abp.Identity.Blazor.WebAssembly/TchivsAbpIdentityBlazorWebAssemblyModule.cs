using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Tchivs.Abp.AspNetCore.Blazor.WebAssembly;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Tchivs.Abp.Identity.Blazor.WebAssembly
{
    [DependsOn(typeof(TchivsAbpIdentityBlazorModule),
        typeof(TchivsAbpAspNetCoreBlazorWebAssemblyModule),
       typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule)
        )]
    public class TchivsAbpIdentityBlazorWebAssemblyModule : AbpModule
    {
    }
}
