using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Blazor.Server;

 public class BlazorGlobalStyleContributor : BundleContributor
{

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Tchivs.Abp.AspNetCore.Blazor/libs/@fortawesome/fontawesome-free/css/all.css");
        context.Files.AddIfNotContains("/_content/Tchivs.Abp.AspNetCore.Blazor/libs/@fortawesome/fontawesome-free/css/v4-shims.css");
        context.Files.AddIfNotContains("/_content/Tchivs.Abp.AspNetCore.Blazor/libs/abp/core/abp.css");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/motronic.min.css");
        // context.Files.AddIfNotContains($"/_content/Tchivs.Abp.AspNetCore.Blazor.Theme.Bootstrap/css/site.css");
    }
}