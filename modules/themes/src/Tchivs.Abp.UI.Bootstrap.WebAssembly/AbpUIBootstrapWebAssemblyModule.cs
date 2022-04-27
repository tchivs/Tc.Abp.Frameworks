using Microsoft.JSInterop;
using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.Bootstrap.WebAssembly
{
    [DependsOn(typeof(AbpUIBootstrapModule))]
    public class AbpUIBootstrapWebAssemblyModule : AbpAspNetCoreComponentsWebAssemblyBasicModule<BootstrapThemeBundleContributor>
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