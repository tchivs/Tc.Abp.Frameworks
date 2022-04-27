using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.WebAssembly;
using Tchivs.Abp.UI.Toolbars;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.MasaBlazor.WebAssembly
{
    [DependsOn(typeof(AbpUIMasaBlazorModule))]
    public class AbpUIMasaBlazorWebAssemblyModule : AbpAspNetCoreComponentsWebAssemblyBasicModule<MasaBlazorBundleContributor>
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
               // options.Contributors.Add(new ToolbarContributor());
            });
        }
    }
}
