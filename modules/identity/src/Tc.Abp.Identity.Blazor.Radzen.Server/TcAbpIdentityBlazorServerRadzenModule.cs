using Tc.Abp.AspNetCore.UI.Server;
using Volo.Abp.Modularity;

namespace Tc.Abp.Identity.Blazor.Radzen.Server;

[DependsOn(typeof(TcAbpIdentityBlazorServerModule),
    typeof(TcAbpIdentityBlazorRadzenModule),
    typeof(TcAbpUIRadzenServerModule))]
public class TcAbpIdentityBlazorServerRadzenModule : AbpModule
{
}

