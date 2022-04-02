using Microsoft.JSInterop;
using Tchivs.Abp.UI;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly
{
    [DependsOn(
    typeof(AbpUIModule),
    typeof(AbpHttpClientIdentityModelWebAssemblyModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyModule)
)]
    public class WebAssemblyThemingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(WebAssemblyThemingModule).Assembly);
            });
        }
    }

}