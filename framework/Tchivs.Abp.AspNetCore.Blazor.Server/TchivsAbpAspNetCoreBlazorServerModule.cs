
using Tchivs.Abp.AspNetCore.Blazor;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Components.Server;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Blazor.Server
{
    [DependsOn(
        typeof(TchivsAbpAspNetCoreBlazorModule),
        typeof(AbpAspNetCoreComponentsServerModule),
        typeof(AbpAspNetCoreMvcUiPackagesModule),
        typeof(AbpAspNetCoreMvcUiBundlingModule)
        )]
    public class TchivsAbpAspNetCoreBlazorServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BlazorThemeBundles.Styles.Global,
                        bundle => { bundle.AddContributors(typeof(BlazorGlobalStyleContributor)); });

                options
                    .ScriptBundles
                    .Add(BlazorThemeBundles.Scripts.Global,
                        bundle => { bundle.AddContributors(typeof(BlazorGlobalScriptContributor)); });
            });
        }
    }
    //public class ToolbarContributor : IToolbarContributor
    //{
    //    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    //    {
    //        if (context.Toolbar.Name == StandardToolbars.Right)
    //        {
    //            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
    //            context.Toolbar.Items.Add(new ToolbarItem(typeof(HeaderUser)));
    //        }

    //        return Task.CompletedTask;
    //    }
    //}
}