using BlazorApp.Host;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tchivs.Abp.UI.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var application = await builder.AddApplicationAsync<BlazorAppHostModule>(options =>
{
    options.UseAutofac();
});

await builder.Build().RunAsync();
