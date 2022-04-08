using Tchivs.Abp.AspNetCore.Components.Server.Bundling;
using Tchivs.Abp.UI;
using Volo.Abp.AspNetCore.Components.Server;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server
{
    [DependsOn(typeof(TchivsAbpUIModule),
    typeof(AbpAspNetCoreComponentsServerModule),
    typeof(AbpAspNetCoreMvcUiPackagesModule),
    typeof(AbpAspNetCoreMvcUiBundlingModule))]
    public class TchivsAbpAspNetCoreComponentsServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BlazorStandardBundles.Styles.Global, bundle =>
                    {
                        bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                    {
                        bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                    });
            });
        }
    }

}