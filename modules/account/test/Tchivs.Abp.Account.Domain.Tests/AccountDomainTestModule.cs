using Tchivs.Abp.Account.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Account
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(AccountEntityFrameworkCoreTestModule)
        )]
    public class AccountDomainTestModule : AbpModule
    {
        
    }
}
