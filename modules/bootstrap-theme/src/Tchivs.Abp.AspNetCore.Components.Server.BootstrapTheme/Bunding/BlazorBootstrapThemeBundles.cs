using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme.Bunding
{

    public class BlazorBootstrapThemeBundles
    {
        public static class Styles
        {
            public static string Global = "Blazor.BootstrapTheme.Global";
        }

        public static class Scripts
        {
            public static string Global = "Blazor.BootstrapTheme.Global";
        }
    }
    public class BlazorBootstrapThemeScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");

        }
    }
    public class BlazorBootstrapThemeStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
            context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/motronic.min.css");
        }
    }
}
