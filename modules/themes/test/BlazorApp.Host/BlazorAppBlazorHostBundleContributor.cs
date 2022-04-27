using Volo.Abp.Bundling;

namespace BlazorApp.Host;

public class BundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("/css/app.css", true);
    }
}