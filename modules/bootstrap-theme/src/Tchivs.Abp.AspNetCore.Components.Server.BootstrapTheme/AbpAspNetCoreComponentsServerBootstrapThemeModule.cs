
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme.Bunding;
using Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme.Components;
using Tchivs.Abp.AspNetCore.Components.Server.Theming;
using Tchivs.Abp.AspNetCore.Components.Server.Theming.Bundling;
using Tchivs.Abp.UI.Bootstrap;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme
{
    [DependsOn(
   typeof(AbpUIBootstrapModule),
   typeof(AbpAspNetCoreComponentsServerThemingModule)
   )]
    public class AbpAspNetCoreComponentsServerBootstrapThemeModule : AbpModule
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
                    .Add(BlazorBootstrapThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                            .AddContributors(typeof(BlazorBootstrapThemeStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorBootstrapThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                            .AddContributors(typeof(BlazorBootstrapThemeScriptContributor));
                    });
            });
        }
    }
    public class ToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name == StandardToolbars.Main)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
            }

            return Task.CompletedTask;
        }
    }

}