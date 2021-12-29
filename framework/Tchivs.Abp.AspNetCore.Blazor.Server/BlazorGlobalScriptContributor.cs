using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.AspNetCore.Blazor.Server;

public class BlazorGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_framework/blazor.server.js");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
        context.Files.AddIfNotContains("/_content/Tchivs.Abp.AspNetCore.Blazor/libs/abp/core/abp.js");
        context.Files.AddIfNotContains("/_content/Tchivs.Abp.AspNetCore.Blazor/js/account-proxy.js");
        context.Files.AddIfNotContains("/_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
    }
}