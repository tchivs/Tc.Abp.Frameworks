using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.IdentityServer
{

    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpAuthorizationAbstractionsModule)
        )]
    public class AbpIdentityServerApplicationContractsModule:AbpModule
    {

    }
}