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
            //context.Files.AddIfNotContains("/_content/Tchivs.Abp.UI/libs/fortawesome/fontawesome-free/css/all.css");
          //  context.Files.AddIfNotContains("/_content/Tchivs.Abp.UI/libs/fortawesome/fontawesome-free/css/v4-shims.css");
        }
    }

}
