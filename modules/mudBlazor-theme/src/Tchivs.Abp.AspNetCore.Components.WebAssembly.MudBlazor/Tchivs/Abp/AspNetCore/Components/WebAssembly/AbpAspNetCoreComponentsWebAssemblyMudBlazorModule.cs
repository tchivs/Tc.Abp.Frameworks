using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly
{
    [DependsOn(typeof(AbpUIMudBlazorModule),
        typeof(TchivsAbpAspNetCoreComponentsWebAssemblyModule))]
    public class AbpWebAssemblyMudBlazorModule : AbpModule
    {
    }
}
