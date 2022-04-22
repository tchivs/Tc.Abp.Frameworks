using Microsoft.OpenApi.Models;
using MudBlazor.Server.Host.Data;
using Tchivs.Abp.AspNetCore.Components.Server;
using Tchivs.Abp.AspNetCore.Components.Server.Bunding;
using Tchivs.Abp.UI;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI.Navigation;

[DependsOn(typeof(AbpServerMudBlazorModule),
typeof(AbpAutofacModule),
typeof(AbpSwashbuckleModule),
typeof(AbpAspNetCoreSerilogModule),
typeof(AbpAspNetCoreMvcUiBasicThemeModule)
//typeof(AbpAspNetCoreMvcClientModule)
//typeof(AbpCachingStackExchangeRedisModule),
    )]
public class MudBlazorServerHostDemoModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        var hostingEnvironment = context.Services.GetHostingEnvironment();
        context.Services.AddSingleton<WeatherForecastService>();
        context.Services.AddTransient(_ => new HttpClient
        {
            BaseAddress = new Uri("/")
        });

      //  Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new AppMenuContributor()); });

        //   ConfigureUrls(configuration);
        ConfigureBundles();
      
        Configure<AbpRouterOptions>(options =>
        {
          //  options.AppAssembly = this.GetType().Assembly;
           options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
        ConfigureLocalizationServices();
        ConfigureRouter(context);
        ConfigureSwaggerServices(context.Services);
    }
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            //BLAZOR UI
            options.StyleBundles.Configure(
               MudBlazorBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/css/site.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/MudBlazor.Server.Host.styles.css");
                }
            );
        });
    }
    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            //因为是前后分离的blazor server项目所以要把这个去掉，否则会提示 Could not find the localization resource XXXX on the remote server!
            //options.GlobalContributors.Remove<RemoteLocalizationContributor>();
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(MudBlazorServerHostDemoModule).Assembly);
        });
    }
    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MudBlazor API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }



    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();
        app.UseAbpRequestLocalization();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
           // app.UseHsts();
        }
        //app.UseHttpsRedirection();
        //app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorApp API"); });
        app.UseConfiguredEndpoints();
    }
}