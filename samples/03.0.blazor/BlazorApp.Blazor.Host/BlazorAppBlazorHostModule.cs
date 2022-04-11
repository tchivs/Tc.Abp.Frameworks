using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Tchivs.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Tchivs.Abp.UI;
using Tchivs.Abp.UI.Components;
using Tchivs.Abp.TenantManagement.Blazor.WebAssembly;

namespace BlazorApp.Blazor.Host;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(AbpIdentityBlazorWebAssemblyBootstrapModule),
   typeof(AbpTenantManagementBlazorWebAssemblyBootstrapModule),
    typeof(AbpAccountApplicationContractsModule)
)]
public class BlazorAppBlazorHostModule : AbpModule
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
        ConfigureAutoMapper(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            // options.AppAssembly = typeof(BlazorAppBlazorHostModule).Assembly;
            options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BlazorAppHostMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.ProviderOptions.DefaultScopes.Add("BlazorApp");
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#ApplicationContainer");
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BlazorAppBlazorHostModule>();
        });
    }
}
