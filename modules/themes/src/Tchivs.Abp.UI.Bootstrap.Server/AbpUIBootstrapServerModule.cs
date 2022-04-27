using Tchivs.Abp.UI.Components;
using Microsoft.JSInterop;
using Tchivs.Abp.AspNetCore.Components.Server;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.Bootstrap.Server
{


    [DependsOn(typeof(AbpUIBootstrapModule))]
    public class AbpUIBootstrapServerModule : AbpAspNetCoreComponentsServerBasicModule<BlazorBootstrapStyleContributor, BlazorBootstrapScriptContributor>
    {

    }
}