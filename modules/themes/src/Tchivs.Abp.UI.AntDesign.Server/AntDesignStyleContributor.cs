using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.AntDesign.Server;

public class AntDesignStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var script in ThemeConst.STYLES)
        {
            context.Files.AddIfNotContains($"/{script}");
        }
    }
}