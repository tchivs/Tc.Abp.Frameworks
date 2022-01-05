using Tchivs.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tchivs.Abp.Account
{
    public abstract class AccountController : AbpControllerBase
    {
        protected AccountController()
        {
            LocalizationResource = typeof(AccountResource);
        }
    }
}
