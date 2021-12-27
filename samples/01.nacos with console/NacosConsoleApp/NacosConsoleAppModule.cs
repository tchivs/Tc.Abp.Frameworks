using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tchivs.Abp.Nacos;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace NacosConsoleApp;

[DependsOn(typeof(AbpAutofacModule), typeof(AbpNacosModule))]
public class NacosConsoleAppModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpNacosOptions>(x =>
        {
            x.UseNaming = true;
            x.UseConfig = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();
       
        context.Services.AddHostedService<NacosConsoleAppService>();
    }
}