using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.Radzen.Server;

public class RadzenStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var style in ThemeConst.STYLES)
        {
            context.Files.AddIfNotContains($"/{style}");
        }
    }
}