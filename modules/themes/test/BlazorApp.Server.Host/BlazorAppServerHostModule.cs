using BlazorApp.Server.Host.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Tchivs.Abp.AspNetCore.Components.Server.Bundling;
using Tchivs.Abp.UI;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
#if BOOTSTRAP
using Tchivs.Abp.UI.Bootstrap.Server;
#elif ANTD
using Tchivs.Abp.UI.AntDesign.Server;
#endif
namespace BlazorApp.Server.Host
{
    [DependsOn(
        typeof(AbpAutofacModule),
#if BOOTSTRAP
typeof(AbpUIBootstrapServerModule),
#elif ANTD
typeof(AbpUIAntDesignServerModule),
#endif
        typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
        typeof(AbpHttpClientIdentityModelWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAspNetCoreMvcClientModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule))]
    public class BlazorAppServerHostModule : AbpModule
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

            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureMenu();
            ConfigureRouter(context);

        }

        private void ConfigureMenu()
        {
            Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new AppMenuContributor()); });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.GlobalContributors.Remove<RemoteLocalizationContributor>();
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
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
        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(BlazorAppServerHostModule).Assembly);
#if BOOTSTRAP

                //  options.AppAssembly =this.GetType().Assembly;
#endif
            });
        }
        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                //BLAZOR UI
                options.StyleBundles.Configure(
                    BlazorStandardBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/css/site.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        //  bundle.AddFiles($"/{this.GetType().Assembly.GetName().Name}.styles.css");
                    }
                );
            });
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
    public class AppMenus
    {
        private const string Prefix = "BlazorApp";

        //Add your menu items here...

    }
}