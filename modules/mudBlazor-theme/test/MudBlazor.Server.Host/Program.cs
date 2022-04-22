using MudBlazor.Server.Host.Data;
using Serilog;
using Serilog.Events;
using System.IO;
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
    .CreateLogger();
try
{
    

    Log.Information("Starting web host.");
    var app = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .AddAppSettingsSecretsJson()

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
