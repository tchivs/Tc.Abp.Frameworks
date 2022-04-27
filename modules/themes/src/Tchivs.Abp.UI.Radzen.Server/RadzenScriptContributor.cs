using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.Radzen.Server;

public class RadzenScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var script in ThemeConst.SCRIPTS)
        {
            context.Files.AddIfNotContains($"/{script}");
        }
    }
}