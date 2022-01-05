using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tchivs.Abp.Account
{
    [DependsOn(
        typeof(AccountDomainSharedModule),
        typeof(Volo.Abp.Account.AbpAccountApplicationContractsModule)
        )]
    public class AccountApplicationContractsModule : AbpModule
    {

    }
}
