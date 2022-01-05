using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Account
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AccountDomainSharedModule)
    )]
    public class AccountDomainModule : AbpModule
    {

    }
}
