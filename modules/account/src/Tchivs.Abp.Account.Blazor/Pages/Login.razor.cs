
using BrowserInterop;
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
using Volo.Abp.Uow;
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
        [NotNull] public LoginInputModel Model { get; set; } = new LoginInputModel();
        [Inject, NotNull] public ISettingProvider? SettingProvider { get; set; }
        [Inject, NotNull] public Controllers.AccountController? AccountController { get; set; }
        [Inject, NotNull] public IOptions<IdentityOptions>? IdentityOptions { get; set; }
        [Inject, NotNull] public IExceptionToErrorInfoConverter? ExceptionToErrorInfoConverter { get; set; }
        [Inject, NotNull] public IAuthenticationSchemeProvider? SchemeProvider { get; set; }
        [Inject, NotNull] public IOptions<AbpAccountOptions> AccountOptions { get; set; }
        //TODO: Why there is an ExternalProviders if only the VisibleExternalProviders is used.
        public IEnumerable<ExternalProviderModel> ExternalProviders { get; set; }
        public IEnumerable<ExternalProviderModel> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));
        public bool EnableLocalLogin { get; set; }
        protected override async Task OnLoadAsync(WindowInterop window)
        {
            try
            {
                this.Model.UserNameOrEmailAddress = await window.LocalStorage.GetItem<string>("username");
                await InvokeAsync(this.StateHasChanged);
            }
            catch (Exception ex)
            {

                await window.Console.Error(ex.Message);
            }
        }
        public virtual async Task OnValidSubmit(EditContext context)
        {
            if (Window != null)
            {
                await Window.LocalStorage.SetItem("username", Model.UserNameOrEmailAddress);
            }
            await Task.Delay(1000);
            var result = await AccountController.Login(new Controllers.Models.UserLoginInfo()
            {
                RememberMe = this.Model.RememberMe,
                UserNameOrEmailAddress = this.Model.UserNameOrEmailAddress,
                Password = this.Model.Password
            });
            if (result.Result == LoginResultType.Success)
            {
                this.Navigation.NavigateTo(this.ReturnUrl ?? "/");
            }
            else
            {
                this.Alerts.Add(Volo.Abp.AspNetCore.Components.Alerts.AlertType.Warning, result.Description);
            }
        }

        private Task OnInvalidSubmit(EditContext context)
        {

            return Task.CompletedTask;
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
