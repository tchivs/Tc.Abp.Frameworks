using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Blazor.WebAssembly
{
    [DependsOn(
        typeof(TchivsAbpAspNetCoreBlazorModule),
    typeof(AbpHttpClientIdentityModelWebAssemblyModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyModule)

    )]
    public class TchivsAbpAspNetCoreBlazorWebAssemblyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
             
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(TchivsAbpAspNetCoreBlazorModule).Assembly;
                options.AdditionalAssemblies.Add(this.GetType().Assembly);
            });

        }
    }
}
