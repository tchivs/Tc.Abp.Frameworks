using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Components;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly.Bootstrap
{
    [DependsOn(
   typeof(AbpUIBootstrapModule),
   typeof(WebAssemblyThemingModule)
   )]
    public class WebAssemblyBootstrapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new ToolbarContributor());
            });
        }
    }
}