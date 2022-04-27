using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Bundling;

namespace Tchivs.Abp.UI.Bootstrap.WebAssembly
{
    public class BootstrapThemeBundleContributor : BasicBundleContributor
    {
        public override void AddScripts(BundleContext context)
        {
            base.AddScripts(context);
            AddScripts(context,ThemeConst.SCRIPTS);
        }

        public override void AddStyles(BundleContext context)
        {
            base.AddStyles(context);
            AddStyles(context, ThemeConst.STYLES);
        }
    }
}