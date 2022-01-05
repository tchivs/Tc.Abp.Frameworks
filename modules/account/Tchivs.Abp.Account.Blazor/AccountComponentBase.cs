using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Volo.Abp.Account.Localization;

namespace Tchivs.Abp.Account.Blazor;

public class AccountComponentBase : TchivsBaseComponent
{
    public AccountComponentBase()
    {
        LocalizationResource = typeof(AccountResource);
        ObjectMapperContext = typeof(TchivsAbpAccountBlazorModule);
    }
}
