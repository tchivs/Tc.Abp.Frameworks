using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Bundling;

namespace Tchivs.Abp.UI.MasaBlazor.WebAssembly;

public class MasaBlazorBundleContributor : BasicBundleContributor
{
    public override void AddScripts(BundleContext context)
    {
        base.AddScripts(context);
        AddScripts(context, ThemeConst.SCRIPTS);
    }

    public override void AddStyles(BundleContext context)
    {
        base.AddStyles(context);
        AddStyles(context, ThemeConst.STYLES);

    }
}