using BlazorApp.Host;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var application = await builder.AddApplicationAsync<BlazorAppHostModule>(options =>
{
    options.UseAutofac();
});

var host = builder.Build();
await application.InitializeApplicationAsync(host.Services);
await host.RunAsync();
