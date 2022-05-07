using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server.Bundling
{
    
    public class BlazorGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            var name = typeof(Abp.UI.TchivsAbpUIModule).Namespace;
            context.Files.AddIfNotContains($"/_content/{name}/libs/fortawesome/css/all.css");
            context.Files.AddIfNotContains($"/_content/{name}/libs/fortawesome/css/v4-shims.css");
        }
    }

}
