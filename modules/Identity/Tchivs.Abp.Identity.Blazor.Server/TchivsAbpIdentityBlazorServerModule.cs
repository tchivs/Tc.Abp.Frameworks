using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Blazor.Server;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Identity.Blazor.Server
{
    [DependsOn(typeof(TchivsAbpIdentityBlazorModule),
        typeof(TchivsAbpAspNetCoreBlazorServerModule))]
    public class TchivsAbpIdentityBlazorServerModule : AbpModule
    {
    }
}
