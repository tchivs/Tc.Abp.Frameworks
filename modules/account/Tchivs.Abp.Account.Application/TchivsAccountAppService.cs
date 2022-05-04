using Tchivs.Abp.Account.Application;
using Tchivs.Abp.Account.Localization;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.Account
{
    public class TchivsAccountAppService: ApplicationService, ITchivsAccountAppService
    {
        public TchivsAccountAppService()
        {
            LocalizationResource = typeof(TchivsAccountResource);
        }
    }
}