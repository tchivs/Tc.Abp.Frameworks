using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Blazor.WebAssembly;

public class BlazorGlobalContributor : IBundleContributor
{
    public string? BasicNameSpace = typeof(TchivsAbpAspNetCoreBlazorModule).Namespace;
    public void AddScripts(BundleContext context)
    {
        foreach (var js in Blazor.Abstractions.ResourceConst.Js)
        {
            context.Add(js);
        }
        context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
        context.Add("_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
    }

    public void AddStyles(BundleContext context)
    {
        foreach (var css in Blazor.Abstractions.ResourceConst.Css)
        {
            context.Add(css);
        }
        context.Add("_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
        context.Add("_content/BootstrapBlazor/css/motronic.min.css");

    }
}