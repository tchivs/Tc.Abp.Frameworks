using Volo.Abp.Bundling;

namespace BlazorApp.Blazor.Host;

public class BlazorAppBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
