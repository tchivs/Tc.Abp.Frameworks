using Volo.Abp.Account;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Account
{
    [DependsOn(typeof(AbpAccountApplicationModule))]
    public class TchivsAbpAccountApplicationModule:AbpModule
    {

    }
}