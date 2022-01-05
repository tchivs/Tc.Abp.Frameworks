using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;

namespace Tchivs.Abp.Account.Accounts
{
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    public interface IAccountService : IApplicationService
    {
    }
    public class UserLoginInfo
    {
        [Required]
        [StringLength(255)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
