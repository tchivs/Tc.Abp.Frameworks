using IdentityServer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
    .WriteTo.Async(c => c.Console())
#endif
    .CreateLogger();
;
try
{
    Log.Information("Starting IdentityServer host.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.AddAppSettingsSecretsJson()
            .UseAutofac()
            .UseSerilog();
    await builder
        .AddApplicationAsync<IdentityServer.IdentityServerHostModule>();
    var app = builder.Build();
    await app.InitializeApplicationAsync();
    await app.RunAsync();
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
 