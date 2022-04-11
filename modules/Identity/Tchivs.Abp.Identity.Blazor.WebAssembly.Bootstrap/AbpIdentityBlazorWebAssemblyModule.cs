using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Tchivs.Abp.AspNetCore.Components.WebAssembly.Bootstrap;
using Tchivs.Abp.Identity.Blazor.Bootstrap;

namespace Tchivs.Abp.Identity.Blazor.WebAssembly
{
    [DependsOn(typeof(AbpIdentityBlazorWebAssemblyModule),
        typeof(AbpIdentityBlazorBootstrpModule)
        ,typeof(WebAssemblyBootstrapModule))]
    public class AbpIdentityBlazorWebAssemblyBootstrapModule : AbpModule
    {
    }
}
