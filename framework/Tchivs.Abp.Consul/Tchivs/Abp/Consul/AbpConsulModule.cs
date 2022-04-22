using System;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Consul
{
    public class AbpConsulModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            var configuration = context.Services.GetConfiguration();
            Configure<AbpConsulOptions>(configuration.GetSection("Consul"));
            context.Services.AddSingleton<ConsulClient>(p =>
            {
                var opt = p.GetRequiredService<IOptionsSnapshot<AbpConsulOptions>>();
                var _consulClient = new ConsulClient(options =>
                {
                    options.Address = new Uri(opt.Value.Service);
                }); return _consulClient;
            });
        }
    }
    public class AbpConsulOptions
    {
        /// <summary>
        /// Consul地址
        /// </summary>
        public string Service { get; set; } = "http://host.docker.internal:8500";
    }
}