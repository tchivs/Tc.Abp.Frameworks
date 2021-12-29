using System.Reflection;

namespace Tchivs.Abp.AspNetCore.Blazor;

public class AbpRouterOptions
{
    public Assembly? AppAssembly { get; set; }

    public List<Assembly> AdditionalAssemblies { get; }

    public AbpRouterOptions()
    {
        AdditionalAssemblies = new List<Assembly>();
    }
}
