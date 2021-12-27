using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore.V2;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Nacos
{
    public class AbpAspNetCoreNacosModule : Volo.Abp.Modularity.AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            var opt = context.Services.ExecutePreConfiguredActions<AbpAspNetCoreNacosOptions>();
            var configuration = context.Services.GetConfiguration();
            if (opt.NacosAspNetOptions == null)
            {
                context.Services.AddNacosAspNet(configuration, opt.SectionName);
            }
            else
            {
                context.Services.AddNacosAspNet(opt.NacosAspNetOptions);
            }
        }
    }

    public class AbpAspNetCoreNacosOptions
    {
        public string SectionName { get; set; } = DefaultName;
        public Action<NacosAspNetOptions>? NacosAspNetOptions { get; set; }
        public static readonly string DefaultName = "nacos";
    }
}