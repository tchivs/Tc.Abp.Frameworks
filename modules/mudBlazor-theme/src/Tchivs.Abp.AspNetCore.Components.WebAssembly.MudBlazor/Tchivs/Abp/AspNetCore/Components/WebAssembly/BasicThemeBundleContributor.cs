using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly
{
    public class BasicThemeBundleContributor : IBundleContributor
    {
        

        public void AddScripts(BundleContext context)
        {
            context.Add("_content/MudBlazor/MudBlazor.min.js");
        }
        public void AddStyles(BundleContext context)
        {
            context.Add("_content/MudBlazor/MudBlazor.min.css");
        }

    }
}
