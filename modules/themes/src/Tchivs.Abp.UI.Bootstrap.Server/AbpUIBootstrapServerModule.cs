using Tchivs.Abp.UI.Components;
using Microsoft.JSInterop;
using Tchivs.Abp.AspNetCore.Components.Server;
using Volo.Abp.Modularity;
using Tchivs.Abp.UI.Toolbars;

namespace Tchivs.Abp.UI.Bootstrap.Server
{


    [DependsOn(typeof(AbpUIBootstrapModule))]
    public class AbpUIBootstrapServerModule : AbpAspNetCoreComponentsServerBasicModule<BlazorBootstrapStyleContributor, BlazorBootstrapScriptContributor>
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new ToolbarContributor());
            });
        }
    }
}