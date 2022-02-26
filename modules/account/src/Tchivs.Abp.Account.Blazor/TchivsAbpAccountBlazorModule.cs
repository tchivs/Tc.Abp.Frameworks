using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Tchivs.Abp.Account.Blazor.ProfileManagement;
using Tchivs.Abp.AspNetCore.Blazor;
using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http.ProxyScripting.Generators.JQuery;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.Account.Blazor
{
    [DependsOn(
        typeof(Tchivs.Abp.AspNetCore.Blazor.TchivsAbpAspNetCoreBlazorModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpIdentityAspNetCoreModule),
        typeof(AbpAutoMapperModule),
         typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpExceptionHandlingModule)
    )]
    public class TchivsAbpAccountBlazorModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AccountResource), typeof(TchivsAbpAccountBlazorModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(TchivsAbpAccountBlazorModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAccountOptions>(options => { 
            });
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TchivsAbpAccountBlazorModule>();
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AbpAccountUserMenuContributor());
            });

            Configure<Tchivs.Abp.AspNetCore.Blazor.Abstractions.AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new AccountModuleToolbarContributor());
            });

            ConfigureProfileManagementPage();

            context.Services.AddAutoMapperObjectMapper<TchivsAbpAccountBlazorModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpAccountBlazorAutoMapperProfile>(validate: true);
            });

            Configure<DynamicJavaScriptProxyOptions>(options =>
            {
                options.DisableModule(AccountRemoteServiceConsts.ModuleName);
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(this.GetType().Assembly);
            });
        }

        private void ConfigureProfileManagementPage()
        {
            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Account/Manage");
            });

            Configure<ProfileManagementPageOptions>(options =>
            {
                options.Contributors.Add(new AccountProfileManagementPageContributor());
            });

            Configure<AbpBundlingOptions>(options =>
            {
                //options.ScriptBundles
                //    .Configure(typeof(ManageModel).FullName,
                //        configuration =>
                //        {
                //            configuration.AddFiles("/client-proxies/account-proxy.js");
                //            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/Password/Default.js");
                //            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.js");
                //        });
            });

        }
    }
}