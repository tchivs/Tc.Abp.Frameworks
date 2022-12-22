using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.AspNetCore.Components.Server;
using Volo.Abp.Modularity;

namespace Tc.Abp.Identity.Blazor;
    [DependsOn(typeof(AbpIdentityBlazorModule),typeof(TcAbpAspNetCoreComponentsServerModule))]
    public class TcAbpIdentityBlazorServerModule:AbpModule
    {
    }
