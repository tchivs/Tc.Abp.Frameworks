using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.AspNetCore.Blazor;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tchivs.Abp.Account.Blazor.IdentityServer
{

    [DependsOn(
     typeof(TchivsAbpAccountBlazorModule),
     typeof(AbpIdentityServerDomainModule)
     )]
    public class AbpAccountBlazorIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpIdentityAspNetCoreOptions>(options =>
            {
                options.ConfigureAuthentication = false;
            });
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountBlazorIdentityServerModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAccountBlazorIdentityServerModule>();
            });

            Configure<IdentityServerOptions>(options =>
            {
                options.UserInteraction.ConsentUrl = "/Consent";
                options.UserInteraction.ErrorUrl = "/Account/Error";
            });
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(this.GetType().Assembly);
            });
            //TODO: Try to reuse from AbpIdentityAspNetCoreModule
            context.Services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
        }
    }
}