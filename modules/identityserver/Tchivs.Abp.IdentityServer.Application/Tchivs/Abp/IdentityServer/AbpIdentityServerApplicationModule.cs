using Volo.Abp.Modularity;
using Volo.Abp.IdentityServer;
using Volo.Abp.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;

namespace Tchivs.Abp.IdentityServer
{
    [DependsOn(typeof(TchivsAbpIdentityServerDomainModule),typeof(AbpIdentityServerApplicationContractsModule),typeof(AbpDddApplicationModule))]
    public class AbpIdentityServerApplicationModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpIdentityServerApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpIdentityServerApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}