using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.MasaBlazor.Server;

public class MasaBlazorStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var css in ThemeConst.STYLES)
        {
            context.Files.AddIfNotContains($"/{css}");
        }
    }
}