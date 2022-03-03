using Tchivs.Abp.AspNetCore.Components.WebAssembly.Theming;
using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Bootstrap;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly.BootstrapTheme
{
    [DependsOn(
   typeof(AbpUIBootstrapModule),
   typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
   )]
    public class AbpAspNetCoreComponentsWebAssemblyBootstrapThemeModule : AbpModule
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