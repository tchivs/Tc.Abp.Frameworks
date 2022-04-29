using Tchivs.Abp.AspNetCore.Components.Server.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server
{
    [DependsOn(typeof(TchivsAbpAspNetCoreComponentsServerModule))]
    public abstract class AbpAspNetCoreComponentsServerBasicModule<TStyleContributor, TScriptContributor> : AbpModule
        where TStyleContributor : BundleContributor
       where TScriptContributor : BundleContributor
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
                        bundle.AddContributors(typeof(TStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                    {
                        bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                        bundle.AddContributors(typeof(TScriptContributor));
                    });
            });
        }
    }
}