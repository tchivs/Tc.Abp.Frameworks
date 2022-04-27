using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.UI.MasaBlazor.Server;

public class MasaBlazorScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        foreach (var script in ThemeConst.SCRIPTS)
        {
            context.Files.AddIfNotContains($"/{script}");
        }
    }
}