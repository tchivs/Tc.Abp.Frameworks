using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.UI.MudBlazor.Server
{
    [DependsOn(typeof(AbpUIMudBlazorModule))]
    public class AbpUIMudBlazorServerModule : AbpAspNetCoreComponentsServerBasicModule<MudBlazorStyleContributor, MudBlazorScriptContributor>
    {

    }
}
