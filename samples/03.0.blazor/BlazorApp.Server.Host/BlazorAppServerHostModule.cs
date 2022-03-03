using BlazorApp.Server.Host.Data;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Tchivs.Abp.UI.Components;
using Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Tchivs.Abp.Identity.Blazor.Server;
using Volo.Abp.Identity;
using Volo.Abp.FeatureManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.UI.Navigation;
using Tchivs.Abp.UI;
using Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme.Bunding;

[DependsOn(typeof(AbpAspNetCoreComponentsServerBootstrapThemeModule),
typeof(AbpAutofacModule),
typeof(AbpSwashbuckleModule),
typeof(AbpAspNetCoreSerilogModule),
typeof(AbpAspNetCoreMvcUiBasicThemeModule),
typeof(AbpAspNetCoreMvcClientModule),
//typeof(AbpCachingStackExchangeRedisModule),
typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
 typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpIdentityBlazorServerModule),
     typeof(AbpIdentityHttpApiClientModule),
     typeof(AbpFeatureManagementHttpApiClientModule),
     typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class BlazorAppServerHostModule : AbpModule
{
    //public override void PreConfigureServices(ServiceConfigurationContext context)
    //{
    //    context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
    //    {
    //        options.AddAssemblyResource(
    //        typeof(Tchivs.Abp.Shared.Localization.BlazorUIResource),
    //            typeof(TchivsAbpSharedModule).Assembly
    //        );
    //    });
    //}
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        var hostingEnvironment = context.Services.GetHostingEnvironment();
        context.Services.AddSingleton<WeatherForecastService>();
        context.Services.AddTransient(_ => new HttpClient
        {
            BaseAddress = new Uri("/")
        });
       
        Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new AppMenuContributor()); });

        //   ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureMultiTenancy();
        ConfigureAuthentication(context, configuration);
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
        ConfigureLocalizationServices();
        ConfigureRouter(context);
        ConfigureSwaggerServices(context.Services);
    }
    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            //options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorBootstrapThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/css/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/BlazorApp.Server.Host.styles.css");
                }
            );
        });
    }
    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
            .AddCookie("Cookies", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
            })
            .AddAbpOpenIdConnect("oidc", options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                options.ClientId = configuration["AuthServer:ClientId"];
                options.ClientSecret = configuration["AuthServer:ClientSecret"];

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("role");
                options.Scope.Add("email");
                options.Scope.Add("phone");
                options.Scope.Add("BlazorApp");
            });
    }

    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            //因为是前后分离的blazor server项目所以要把这个去掉，否则会提示 Could not find the localization resource XXXX on the remote server!
            options.GlobalContributors.Remove<RemoteLocalizationContributor>();
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });
    }
    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = false;
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(BlazorAppServerHostModule).Assembly);
        });
    }
    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorApp API", Version = "v1" });
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
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        //app.UseCors();
        app.UseAuthentication();
        //if (MultiTenancyConsts.IsEnabled)
        //{
        //    app.UseMultiTenancy();
        //}
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorApp API"); });
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
public class AppMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
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
        // if (MultiTenancyConsts.IsEnabled)
        // {
        //     administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        // }
        // else
        // {
        //     administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        // }
        //
        // administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        // administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}