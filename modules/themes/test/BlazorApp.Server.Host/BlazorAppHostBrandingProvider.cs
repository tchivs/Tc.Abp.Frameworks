using Volo.Abp.Ui.Branding;

namespace BlazorApp.Server.Host;

public class BlazorAppHostBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BlazorApp|ssr";
}