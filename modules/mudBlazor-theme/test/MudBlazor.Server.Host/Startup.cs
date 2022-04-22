public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<MudBlazorServerHostDemoModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}