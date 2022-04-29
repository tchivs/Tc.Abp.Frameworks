using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Tchivs.Abp.Identity.Blazor.Bootstrap;
using Tchivs.Abp.UI.Bootstrap.WebAssembly;

namespace Tchivs.Abp.Identity.Blazor.WebAssembly
{
    [DependsOn(typeof(AbpIdentityBlazorWebAssemblyModule),
        typeof(AbpIdentityBlazorBootstrpModule)
        ,typeof(AbpUIBootstrapWebAssemblyModule))]
    public class AbpIdentityBlazorWebAssemblyBootstrapModule : AbpModule
    {
    }
}
