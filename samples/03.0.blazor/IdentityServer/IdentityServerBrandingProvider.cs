using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace IdentityServer;

[Dependency(ReplaceServices = true)]
public class IdentityServerBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IdentityServer";
}
