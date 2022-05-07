using System;
using Volo.Abp.Bundling;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly
{
    public class BasicBundleContributor : IBundleContributor
    {
        public virtual void AddScripts(BundleContext context)
        {
            context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
            context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
            context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/lang-utils.js");
        }
        public   void AddScripts(BundleContext context,params string[] scripts)
        {
            foreach (var script in scripts)
            {
                context.Add(script);
            }
        }
        public void AddStyles(BundleContext context, params string[] stypes)
        {
            foreach (var style in stypes)
            {
                context.Add(style);
            }
        }
        public virtual void AddStyles(BundleContext context)
        {
             var name = typeof(Abp.UI.TchivsAbpUIModule).Namespace;
            context.BundleDefinitions.Insert(0, new BundleDefinition
            {
                Source = $"_content/{name}/libs/fortawesome/css/all.css"
            });
            context.BundleDefinitions.Insert(0, new BundleDefinition
            {
                Source = $"_content/{name}/libs/fortawesome/css/v4-shims.css"
            });
        }
    }

}