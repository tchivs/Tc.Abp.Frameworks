using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server.Bunding;
using Tchivs.Abp.AspNetCore.Components.Server.Bundling;
using Tchivs.Abp.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server
{
    [DependsOn(typeof(AbpUIMudBlazorModule),
        typeof(TchivsAbpAspNetCoreComponentsServerModule))]
    public class AbpServerMudBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(MudBlazorBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                            .AddContributors(typeof(MudBlazorStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(MudBlazorBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                            .AddContributors(typeof(MudBlazorScriptContributor));
                    });
            });
        }
        }
}
