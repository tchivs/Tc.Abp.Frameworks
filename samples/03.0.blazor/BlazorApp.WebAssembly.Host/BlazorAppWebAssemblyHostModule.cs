using AutoMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tchivs.Abp.AspNetCore.Blazor;
using Tchivs.Abp.AspNetCore.Blazor.Components;
using Tchivs.Abp.AspNetCore.Blazor.WebAssembly;
using Volo.Abp.Account;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.Ui.Branding;
using Volo.Abp.UI.Navigation;
public class BlazorAppHostBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BlazorApp|Wasm";
}
[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(TchivsAbpAspNetCoreBlazorWebAssemblyModule)
)]
public class BlazorAppWebAssemblyHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();
        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureRouter(context);
        ConfigureUi(builder);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            
            options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
    }
    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        //Configure<AbpNavigationOptions>(options =>
        //{
        //    options.MenuContributors.Add(new DemoAppHostMenuContributor(context.Services.GetConfiguration()));
        //});
    }
    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);

            options.ProviderOptions.DefaultScopes.Add("BlazorApp");
        });
    }
    private static void ConfigureUi(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
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
            options.AddMaps<BlazorAppWebAssemblyHostModule>();
        });
    }
}

public class BlazorAppWebAssemblyHostAutoMapperProfile : Profile
{

}