using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.AspNetCore.UI.WebAssembly;
using Volo.Abp.Modularity;

namespace Tc.Abp.Identity.Blazor.Radzen.WebAssembly;

[DependsOn(typeof(TcAbpIdentityBlazorWebAssemblyModule),
    typeof(TcAbpIdentityBlazorRadzenModule),
    typeof(TcAbpUIMudBlazorWebAssemblyModule))]
public class TcAbpIdentityBlazorWebAssemblyRadzenModule : AbpModule
{
}

