using BlazorApp.Server.Host.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Tchivs.Abp.AspNetCore;
using Tchivs.Abp.AspNetCore.Blazor;
using Tchivs.Abp.AspNetCore.Blazor.Server;
using Tchivs.Abp.Core;
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

[DependsOn(typeof(TchivsAbpAspNetCoreBlazorServerModule),
typeof(AbpAutofacModule),
typeof(AbpSwashbuckleModule),
typeof(AbpAspNetCoreSerilogModule),
//typeof(AbpCachingStackExchangeRedisModule),
typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
 typeof(AbpHttpClientIdentityModelWebModule),
typeof(AbpAspNetCoreSerilogModule)
//typeof(AbpIdentityHttpApiClientModule)
)]
public class BlazorAppServerHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAuthorizationCore().AddSingleton<WeatherForecastService>();


        Configure<AbpBundlingOptions>(options =>
        {
            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/css/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/BlazorApp.Server.Host.styles.css");
                }
            );
        });
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = this.GetType().Assembly;
        });
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
        context.Services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorApp API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "¼òÌåÖÐÎÄ"));
        });
        Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = false; });

        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri("/")
        });
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
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
        app.UseCors();
        app.UseAuthentication();
        //app.UseJwtTokenMiddleware();
        //if (MultiTenancyConsts.IsEnabled)
        //{
        //    app.UseMultiTenancy();
        //}
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoApp API"); });
        app.UseConfiguredEndpoints();
    }
}