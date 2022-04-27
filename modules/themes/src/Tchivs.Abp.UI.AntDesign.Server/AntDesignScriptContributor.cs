using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.AntDesign.Server;

public class AntDesignScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var script in ThemeConst.SCRIPTS)
        {
            context.Files.AddIfNotContains($"/{script}");
        }
    }
}