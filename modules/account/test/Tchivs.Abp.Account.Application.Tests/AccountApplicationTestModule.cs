using Volo.Abp.Modularity;

namespace Tchivs.Abp.Account
{
    [DependsOn(
        typeof(AccountApplicationModule),
        typeof(AccountDomainTestModule)
        )]
    public class AccountApplicationTestModule : AbpModule
    {

    }
}
