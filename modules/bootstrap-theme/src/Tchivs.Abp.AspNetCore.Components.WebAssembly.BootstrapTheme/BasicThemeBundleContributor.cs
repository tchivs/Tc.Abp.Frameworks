using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly.BootstrapTheme
{
    public class BasicThemeBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
            context.Add("_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
            context.Add("_content/BootstrapBlazor/css/motronic.min.css");
            context.Add("_content/Tchivs.Abp.UI.Bootstrap/css/global.css");
        }

    }
}