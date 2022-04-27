using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.Radzen.Server
{
    [DependsOn(typeof(AbpUIRadzenModule))]
    public class AbpUIRadzenServerModule : AbpAspNetCoreComponentsServerBasicModule<RadzenStyleContributor, RadzenScriptContributor>
    {

    }
}
