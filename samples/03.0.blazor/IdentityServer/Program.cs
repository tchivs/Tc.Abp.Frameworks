using IdentityServer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
#else
                .MinimumLevel.Information()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.WithProperty("Application", "BlazorApp")
    .Enrich.FromLogContext()
#if DEBUG
    .WriteTo.Async(c => c.File("Logs/logs.txt"))
    .WriteTo.Async(c => c.Console())
#endif
    .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                        IndexFormat = "blazorApp-log-{0:yyyy.MM}"
                    })
    .CreateLogger();
try
{
    Log.Information("Starting web host.");
    var app = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .AddAppSettingsSecretsJson()
        .ConfigureAppConfiguration(build =>
        {
            build.AddJsonFile("appsettings.secrets.json", optional: true);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .UseAutofac()
        .UseSerilog().Build();
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IdentityServerHostModule>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.InitializeApplication();
    }
}