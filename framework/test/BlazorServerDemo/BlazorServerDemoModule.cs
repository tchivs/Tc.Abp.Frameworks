using Microsoft.OpenApi.Models;
using Tc.Abp.AspNetCore;
using Tc.Abp.AspNetCore.Components.Server;
using Tc.Abp.AspNetCore.UI.Server;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;

[DependsOn(
    typeof(TcAbpAspNetCoreComponentsServerModule),
    typeof(TcAbpUIBootstrapBlazorServerModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
public class BlazorServerDemoModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(BlazorServerDemoModule)

            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureSwaggerServices(context.Services);
        ConfigureUI(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
    }
    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            //options.StyleBundles.Configure(
            //    BlazorStandardBundles.Styles.Global,
            //    bundle =>
            //    {
            //        bundle.AddFiles("/global-styles.css");
            //    }
            //);

            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorStandardBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/css/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles($"/{this.GetType().Namespace}.styles.css");
                }
            );
            options.ScriptBundles.Configure(
               BlazorStandardBundles.Styles.Global,
               bundle =>
               {
                   //bundle.AddFiles("/_content/MudBlazor/MudBlazor.min.js");
                   //bundle.AddFiles("/_content/Blazor.NPlayer/nplayer.bundle.min.js");
               }
           );
        });
    }
    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Spiders API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureUI(ServiceConfigurationContext context)
    {
        //context.Services
        //    .AddBootstrap5Providers()
        //    .AddFontAwesomeIcons();
    }
    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            //options.MenuContributors.Add(new SpiderMenuContributor());
        });
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(this.GetType().Assembly);
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BlazorServerDemoModule>();
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
        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Spider API");
        });
        app.UseConfiguredEndpoints();
    }
}