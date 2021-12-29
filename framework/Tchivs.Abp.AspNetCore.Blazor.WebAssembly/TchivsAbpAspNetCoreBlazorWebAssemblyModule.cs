using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Blazor.WebAssembly
{
    [DependsOn(
        typeof(TchivsAbpAspNetCoreBlazorModule)

    )]
    public class TchivsAbpAspNetCoreBlazorWebAssemblyModule : AbpModule
    {

    }
}
