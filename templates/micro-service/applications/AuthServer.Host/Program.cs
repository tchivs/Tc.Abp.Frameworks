//TODO: Temporary: it's not good to read appsettings.json here just to configure logging

using AuthServer.Host;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.WithProperty("Application", "AuthServer")
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/logs.txt")
#if DEBUG
    .WriteTo.Async(c => c.Console())
#endif
    .WriteTo.Elasticsearch(
        new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            IndexFormat = "AuthServer.Host-log-{0:yyyy.MM}"
        })
    .CreateLogger();
try
{
    Log.Information("Starting AuthServer.Host.");
    Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .UseAutofac()
        .UseSerilog()
        .Build()
        .Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "AuthServer.Host terminated unexpectedly!");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
