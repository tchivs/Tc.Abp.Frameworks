using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.UI;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.Identity.Blazor.Bootstrap;

[DependsOn(typeof(AbpIdentityBlazorModule),
    typeof(AbpUIBootstrapModule))]
public class AbpIdentityBlazorBootstrpModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpIdentityBlazorBootstrpModule).Assembly);
        });
    }
}
