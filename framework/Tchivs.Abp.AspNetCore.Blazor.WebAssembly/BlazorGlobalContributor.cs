using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Blazor.WebAssembly;

public class BlazorGlobalContributor : IBundleContributor
{
    public string? BasicNameSpace = typeof(TchivsAbpAspNetCoreBlazorModule).Namespace;
    public void AddScripts(BundleContext context)
    {
        context.Add($"_content/{BasicNameSpace}/libs/abp/core/abp.js");
        context.Add($"_content/{BasicNameSpace}/js/account-proxy.js");
        context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
        context.Add("_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.BundleDefinitions.Insert(0, new BundleDefinition
        {
            Source = $"_content/{BasicNameSpace}/libs/@fortawesome/fontawesome-free/css/all.css"
        });
        context.BundleDefinitions.Insert(1, new BundleDefinition
        {
            Source = $"_content/{BasicNameSpace}/libs/@fortawesome/fontawesome-free/css/v4-shims.css"
        });
        context.Add($"_content/{BasicNameSpace}/libs/abp/core/abp.css");
        context.Add("_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
        context.Add("_content/BootstrapBlazor/css/motronic.min.css");

    }
}