using System;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.Modularity;
using Tchivs.Abp.Consul;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tchivs.Abp.AspNetCore.Consul
{
    [DependsOn(typeof(AbpConsulModule))]
    public class AbpAspNetCoreConsulModule : AbpModule
    {
        private ConsulClient client;
        private AgentServiceRegistration registration;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<AbpAspNetCoreConsulOptions>(configuration.GetSection("Consul"));
            var options = new AbpAspNetCoreConsulOptions();
            configuration.Bind("Consul", options);
            client = new ConsulClient(opt =>
          {
              opt.Address = new Uri(options.Address); // Consul客户端地址
            });
            registration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(), // 唯一Id
                Name = options.Name, // 服务名
                Address = options.Address, // 服务绑定IP
                Port = options.Port, // 服务绑定端口
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔
                    HTTP = options.GetHealthUrl(), // 健康检查地址
                    Timeout = TimeSpan.FromSeconds(options.Timeout) // 超时时间
                }
            };
            // 注册服务
            client.Agent.ServiceRegister(registration).Wait();
        }
        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            //应用程序终止时，取消服务注册
            client.Agent.ServiceDeregister(registration.ID).Wait();
        }
    }
}