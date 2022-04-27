using Volo.Abp.Ui.Branding;

namespace BlazorApp.Host;

public class BlazorAppHostBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BlazorApp|wasm";
}