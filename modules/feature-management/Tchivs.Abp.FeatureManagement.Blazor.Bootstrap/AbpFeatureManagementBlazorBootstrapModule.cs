using Microsoft.JSInterop;
using Tchivs.Abp.UI;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.FeatureManagement.Blazor
{
    [DependsOn(typeof(AbpUIBootstrapModule),
        typeof(AbpFeatureManagementBlazorModule))]

    public class AbpFeatureManagementBlazorBootstrapModule : AbpModule 
    {
     
    }

}