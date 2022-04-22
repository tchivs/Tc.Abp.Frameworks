using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.Server.Bunding
{
    public class MudBlazorScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/MudBlazor/MudBlazor.min.js");

        }
    }
}
