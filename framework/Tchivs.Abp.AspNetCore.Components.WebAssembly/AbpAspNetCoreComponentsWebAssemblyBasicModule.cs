using Tchivs.Abp.UI;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Bundling;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.WebAssembly
{
    public class TchivsAbpAspNetCoreComponentsWebAssemblyModule : AbpModule

    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(TchivsAbpAspNetCoreComponentsWebAssemblyModule).Assembly);
            });
        }
    }
    [DependsOn(
  typeof(TchivsAbpUIModule),
  typeof(AbpHttpClientIdentityModelWebAssemblyModule),
  typeof(TchivsAbpAspNetCoreComponentsWebAssemblyModule),
  typeof(AbpAspNetCoreComponentsWebAssemblyModule)
)]
    public abstract class AbpAspNetCoreComponentsWebAssemblyBasicModule<TBundleContributor> : AbpModule
        where TBundleContributor : IBundleContributor
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(this.GetType().Assembly);
            });
        }
    }
}