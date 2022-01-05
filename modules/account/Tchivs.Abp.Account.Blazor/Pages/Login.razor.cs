
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tchivs.Abp.Account.Blazor.Controllers.Models;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Settings;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using static Tchivs.Abp.Account.Blazor.Pages.LoginModel;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Tchivs.Abp.Account.Blazor.Pages
{
    public partial class Login
    {
        [Parameter] public string? ReturnUrl { get; set; }
        [Parameter] public string? ReturnUrlHash { get; set; }
        [NotNull] public LoginInputModel? Model { get; set; } = new LoginInputModel();
        [Inject, NotNull] public ISettingProvider? SettingProvider { get; set; }
        [Inject, NotNull] public IAccountAppService? AccountAppService { get; set; }
        [Inject, NotNull] public SignInManager<IdentityUser>? SignInManager { get; set; }
        [Inject, NotNull] public IdentityUserManager? UserManager { get; set; }
        [Inject, NotNull] public IdentitySecurityLogManager? IdentitySecurityLogManager { get; set; }
        [Inject, NotNull] public IOptions<IdentityOptions>? IdentityOptions { get; set; }
        [Inject, NotNull] public IExceptionToErrorInfoConverter? ExceptionToErrorInfoConverter { get; set; }
        [Inject, NotNull] public IAuthenticationSchemeProvider? SchemeProvider { get; set; }
        [Inject, NotNull] public IOptions<AbpAccountOptions> AccountOptions { get; set; }
        //TODO: Why there is an ExternalProviders if only the VisibleExternalProviders is used.
        public IEnumerable<ExternalProviderModel> ExternalProviders { get; set; }
        public IEnumerable<ExternalProviderModel> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));
        public bool EnableLocalLogin { get; set; }
        private async Task OnValidSubmit(EditContext context)
        {
              
            await CheckLocalLoginAsync();
            ValidateLoginInfo(Model);

            ExternalProviders = await GetExternalProviders();
            EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);

            await ReplaceEmailToUsernameOfInputIfNeeds(Model);
            await IdentityOptions.SetAsync();
            var result = await SignInManager.PasswordSignInAsync(
           Model.UserNameOrEmailAddress,
           Model.Password,
           Model.RememberMe,
           true
            );
            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = result.ToIdentitySecurityLogAction(),
                UserName = Model.UserNameOrEmailAddress
            });
            if (result.RequiresTwoFactor)
            {
                await TwoFactorLoginResultAsync();
            }

            if (result.IsLockedOut)
            {
                Alerts.Warning(L["UserLockedOutMessage"]);
                return;
            }

            if (result.IsNotAllowed)
            {
                Alerts.Warning(L["LoginIsNotAllowed"]);
                return;
            }

            if (!result.Succeeded)
            {
                Alerts.Danger(L["InvalidUserNameOrPassword"]);
                return;
            }
            //TODO: Find a way of getting user's id from the logged in user and do not query it again like that!
            var user = await UserManager.FindByNameAsync(Model.UserNameOrEmailAddress) ??
                       await UserManager.FindByEmailAsync(Model.UserNameOrEmailAddress);
            Debug.Assert(user != null, nameof(user) + " != null");
            this.Navigation.NavigateTo(ReturnUrl ?? "/");
            //  return RedirectSafely(ReturnUrl, ReturnUrlHash);
        }

        private Task OnInvalidSubmit(EditContext context)
        {
            
            return Task.CompletedTask;
        }
       
        /// <summary>
        /// Override this method to add 2FA for your application.
        /// </summary>
        protected virtual Task<IActionResult> TwoFactorLoginResultAsync()
        {
            throw new NotImplementedException();
        }
        protected virtual async Task<List<ExternalProviderModel>> GetExternalProviders()
        {
            var schemes = await SchemeProvider.GetAllSchemesAsync();

            return schemes
                .Where(x => x.DisplayName != null || x.Name.Equals(AccountOptions.Value.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                .Select(x => new ExternalProviderModel
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                })
                .ToList();
        }
        protected virtual async Task ReplaceEmailToUsernameOfInputIfNeeds(LoginInputModel login)
        {
            if (!ValidationHelper.IsValidEmailAddress(login.UserNameOrEmailAddress))
            {
                return;
            }

            var userByUsername = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);
            if (userByUsername != null)
            {
                return;
            }

            var userByEmail = await UserManager.FindByEmailAsync(login.UserNameOrEmailAddress);
            if (userByEmail == null)
            {
                return;
            }

            login.UserNameOrEmailAddress = userByEmail.UserName;
        }
        protected virtual void ValidateLoginInfo(LoginInputModel login)
        {
            if (login == null)
            {
                throw new ArgumentException(nameof(login));
            }

            if (login.UserNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(login.UserNameOrEmailAddress));
            }

            if (login.Password.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(login.Password));
            }
        }
        protected virtual async Task CheckLocalLoginAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
            {
                throw new UserFriendlyException(L["LocalLoginDisabledMessage"]);
            }
        }

    }
}
