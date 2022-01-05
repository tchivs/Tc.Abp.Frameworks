using Volo.Abp.Modularity;
using Dapr.AspNetCore;
namespace Tchivs.Abp.AspNetCore.Dapr
{
    [DependsOn(typeof(Volo.Abp.AspNetCore.AbpAspNetCoreModule))]
    public class TchivsAbpAspNetCoreDaprModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddDapr();
        }
    }
}