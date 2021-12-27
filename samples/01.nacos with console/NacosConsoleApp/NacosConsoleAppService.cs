using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.V2;

namespace NacosConsoleApp;

public class NacosConsoleAppService : BackgroundService
{
    private readonly INacosConfigService _nacosConfigService;
    private readonly INacosNamingService _nacosNamingService;
    private readonly ILogger<NacosConsoleAppService> _logger;

    public NacosConsoleAppService(
        INacosConfigService nacosConfigService,
        INacosNamingService nacosNamingService,
        ILogger<NacosConsoleAppService> logger)
    {
        _nacosConfigService = nacosConfigService;
        _nacosNamingService = nacosNamingService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //get config
        var config = await _nacosConfigService.GetConfig("redis", "master", 1000);
        _logger.LogInformation(config);

        //get naming 
        var instance = await _nacosNamingService.SelectOneHealthyInstance("test", "master");
        var host = $"{instance.Ip}:{instance.Port}";
        var baseUrl = instance.Metadata.TryGetValue("secure", out _)
            ? $"https://{host}"
            : $"http://{host}";
        _logger.LogInformation($"instance:{host}\t{baseUrl}\n");
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new NullReferenceException(nameof(baseUrl));
        }
        var url = $"{baseUrl}/api/values";

        using HttpClient client = new HttpClient();
        var result = await client.GetAsync(url, stoppingToken);
        var str = await result.Content.ReadAsStringAsync(stoppingToken);
        _logger.LogInformation($"get result:{url}\t{str}\n");
    }
}