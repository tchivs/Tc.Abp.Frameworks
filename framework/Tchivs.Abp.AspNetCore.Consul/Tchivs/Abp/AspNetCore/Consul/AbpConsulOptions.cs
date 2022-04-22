using Tchivs.Abp.Consul;

namespace Tchivs.Abp.AspNetCore.Consul
{
    public class AbpAspNetCoreConsulOptions: AbpConsulOptions
    {
        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool Ssl { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; } = 5;
        /// <summary>
        /// 健康检查时间间隔
        /// </summary>
        public int Interval { get; set; } = 10;
        /// <summary>
        /// 服务绑定端口
        /// </summary>
        public int Port { get; set; } = 80;
        /// <summary>
        /// 服务绑定IP
        /// </summary>
        public string Address { get; set; } = "host.docker.internal";

        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheck { get; set; } = "/healthcheck";
        public string GetHealthUrl()
        {
            var prefix = Ssl?"https://":"http://";
            return $"{prefix}{Address}:{Port}{HealthCheck}";
        }
    }
}