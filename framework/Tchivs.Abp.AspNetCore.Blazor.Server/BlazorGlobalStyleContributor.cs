using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Blazor.Server;

public class BlazorGlobalStyleContributor : BundleContributor
{

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
    
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/motronic.min.css");
        foreach (var css in Blazor.Abstractions.ResourceConst.Css)
        {
            context.Files.AddIfNotContains($"/{css}");
        }
    }
}