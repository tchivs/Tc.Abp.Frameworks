using Tchivs.Abp.Core;
using Volo.Abp.Modularity;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tchivs.Abp.FreeSql
{
    [DependsOn(typeof(TchivsAbpCoreModule))]
    public class TchivsAbpFreeSqlModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();
            this.Configure<Tchivs.Abp.FreeSql.ConnectionOptions>(configuration.GetSection(nameof(ConnectionOptions)));
            context.Services.AddSingleton(p =>
            {
                return p.GetRequiredService<IFreeSqlSelector>().GetFreeSql();
            });
        }
        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            context.ServiceProvider.GetRequiredService<IFreeSqlConnectPool>().Dispose();
        }
    }
}
