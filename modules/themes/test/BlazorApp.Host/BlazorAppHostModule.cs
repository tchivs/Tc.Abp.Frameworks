using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tchivs.Abp.UI;
#if BOOTSTRAP
using Tchivs.Abp.UI.Bootstrap.WebAssembly;
#endif
using Tchivs.Abp.UI.Components;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace BlazorApp.Host;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
#if BOOTSTRAP
    typeof(AbpUIBootstrapWebAssemblyModule),
#endif
    typeof(AbpAccountApplicationContractsModule)
)]
public class BlazorAppHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();
        ConfigureAuthentication(builder);
       ConfigureHttpClient(context, environment);
        ConfigureRouter(context);
        ConfigureUI(builder);
        ConfigureMenu(context);
    }
    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BlazorAppHostMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
    }
    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.ProviderOptions.DefaultScopes.Add("BlazorApp");
        });
    }
    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
    }

    public class BlazorAppHostMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public BlazorAppHostMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }
        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            context.Menu.Items.Insert(0,
                new ApplicationMenuItem("Index", displayName: "Index", "/", icon: "fa fa-home"));
            return Task.CompletedTask;
        }
        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            context.Menu.AddItem(new ApplicationMenuItem(
                "Account.Manage",
                accountStringLocalizer["ManageYourProfile"],
                $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
                icon: "fa fa-cog",
                order: 1000,
                null).RequireAuthenticated());

            return Task.CompletedTask;
        }
    }

}