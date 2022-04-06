using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.Server.Bootstrap.Bunding
{
    public class BlazorBootstrapStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/motronic.min.css");
            context.Files.AddIfNotContains("/_content/Tchivs.Abp.UI.Bootstrap/css/global.css");
        }
    }
}
