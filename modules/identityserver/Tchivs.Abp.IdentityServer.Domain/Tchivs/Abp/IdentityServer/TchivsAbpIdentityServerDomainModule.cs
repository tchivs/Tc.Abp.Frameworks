using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.IdentityServer
{
    [DependsOn(typeof(AbpIdentityServerDomainModule),typeof(TchivsAbpIdentityServerDomainSharedModule))]
    public class TchivsAbpIdentityServerDomainModule:AbpModule
    {
    }
}
