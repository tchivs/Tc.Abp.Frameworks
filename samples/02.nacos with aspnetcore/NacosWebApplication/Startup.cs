public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<NacosWebApplicationModule>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}