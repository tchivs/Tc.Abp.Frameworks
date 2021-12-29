using BlazorApp.WebAssembly.Host;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var application = builder.AddApplication<BlazorAppWebAssemblyHostModule>(options =>
{
    options.UseAutofac();
});
var host = builder.Build();
await application.InitializeAsync(host.Services);
//builder.RootComponents.Add<HeadOutlet>("head::after");
await host.RunAsync();