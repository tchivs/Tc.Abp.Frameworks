using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server;
using Tchivs.Abp.Identity.Blazor.Bootstrap;
using Tchivs.Abp.Identity.Blazor.Server;
using Tchivs.Abp.UI.Bootstrap.Server;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Identity.Blazor.Server.Bootstrap
{
    [DependsOn(typeof(AbpIdentityBlazorServerModule),
        typeof(AbpIdentityBlazorBootstrpModule),
        typeof(AbpUIBootstrapServerModule))]
    public class AbpIdentityBlazorServerBootstrapModule : AbpModule
    {
    }
}
