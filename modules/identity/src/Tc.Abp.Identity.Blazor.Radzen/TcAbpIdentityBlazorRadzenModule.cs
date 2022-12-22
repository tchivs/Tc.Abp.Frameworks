using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.AspNetCore.UI;
using Volo.Abp.Modularity;

namespace Tc.Abp.Identity.Blazor.Radzen;

[DependsOn(typeof(AbpIdentityBlazorModule),typeof(TcAbpUIRadzenModule))]
public class TcAbpIdentityBlazorRadzenModule : AbpModule
{
}

