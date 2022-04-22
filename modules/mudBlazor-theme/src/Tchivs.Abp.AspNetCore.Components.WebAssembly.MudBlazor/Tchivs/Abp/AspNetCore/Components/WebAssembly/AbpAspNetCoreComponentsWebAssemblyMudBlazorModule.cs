
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
