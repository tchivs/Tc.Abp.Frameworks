using Tchivs.Abp.AspNetCore.Components.Server.Bootstrap.Bunding;
using Tchivs.Abp.AspNetCore.Components.Server;
using Tchivs.Abp.AspNetCore.Components.Server.Bundling;
using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server.Bootstrap
{
    [DependsOn(
   typeof(AbpUIBootstrapModule),
   typeof(ServerThemingModule)
   )]
    public class ServerBootstrapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new ToolbarContributor());
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BlazorBootstrapBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                            .AddContributors(typeof(BlazorBootstrapStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorBootstrapBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                            .AddContributors(typeof(BlazorBootstrapScriptContributor));
                    });
            });
        }
    }

}