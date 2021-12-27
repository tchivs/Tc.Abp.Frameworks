using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2;
using Nacos.V2.DependencyInjection;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Nacos;
/// <summary>
/// Abp Nacos模块
/// </summary>
public class AbpNacosModule : Volo.Abp.Modularity.AbpModule
{

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var opt = context.Services.ExecutePreConfiguredActions<AbpNacosOptions>();
        if (!opt.UseConfig && !opt.UseNaming)
        {
            throw new ApplicationException($"{nameof(AbpNacosOptions)} is disable！！");
        }
        if (opt.UseConfig)
        {
            if (opt.ConfigConfigureAction != null)
            {
                context.Services.AddNacosV2Config(opt.ConfigConfigureAction);
            }
            else
            {

                context.Services.AddNacosV2Config(configuration, null, opt.ConfigSectionName);

            }
        }
        if (opt.UseNaming)
        {
            if (opt.NamingConfigureAction != null)
            {
                context.Services.AddNacosV2Config(opt.NamingConfigureAction);
            }
            else
            {
                context.Services.AddNacosV2Naming(configuration, null, opt.NamingSectionName);
            }
        }
    }
}
