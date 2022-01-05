using Volo.Abp.Account.Localization;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.Account
{
    public abstract class AccountAppService : ApplicationService
    {
        protected AccountAppService()
        {
            LocalizationResource = typeof(AccountResource);
            ObjectMapperContext = typeof(AccountApplicationModule);
        }
    }
}
