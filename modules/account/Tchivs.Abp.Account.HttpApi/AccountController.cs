using Microsoft.AspNetCore.Mvc;
using Tchivs.Abp.Account.Application;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;

namespace Tchivs.Abp.Account.HttpApi
{
    [Area(AccountRemoteServiceConsts.ModuleName)]
    [Route("api/account")]
    public class AccountController : AbpControllerBase, ITchivsAccountAppService
    {
    }
    }
