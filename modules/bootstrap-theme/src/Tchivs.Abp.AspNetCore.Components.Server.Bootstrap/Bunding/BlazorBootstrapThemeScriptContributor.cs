using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.Server.Bootstrap.Bunding
{
    public class BlazorBootstrapScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");

        }
    }
}
