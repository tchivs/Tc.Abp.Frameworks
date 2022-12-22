using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tc.Abp.Identity.Blazor;
    [DependsOn(typeof(AbpIdentityBlazorModule)  ,   typeof(AbpIdentityHttpApiClientModule),
       typeof(AbpPermissionManagementHttpApiClientModule))]
    public class TcAbpIdentityBlazorWebAssemblyModule:AbpModule
    {
    }
