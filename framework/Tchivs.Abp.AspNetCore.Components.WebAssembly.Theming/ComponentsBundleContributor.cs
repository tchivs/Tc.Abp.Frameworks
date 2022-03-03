using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly.Theming
{
    public class ComponentsBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
            context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
            context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
            context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/lang-utils.js");
        }

        public void AddStyles(BundleContext context)
        {
            var name = this.GetType().Namespace;
            context.BundleDefinitions.Insert(0, new BundleDefinition
            {
                Source = $"_content/{name}/libs/fontawesome/css/all.css"
            });

            context.Add($"_content/{name}/libs/flag-icon/css/flag-icon.css");
        }
    }

}