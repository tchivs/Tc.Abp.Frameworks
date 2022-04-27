namespace BlazorApp.Server.Host;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<BlazorAppServerHostModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}