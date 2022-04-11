using Tchivs.Abp.UI;
using Volo.Abp.Modularity;
using Volo.Abp.FeatureManagement;

namespace Tchivs.Abp.FeatureManagement.Blazor
{
    [DependsOn(typeof(TchivsAbpUIModule),
        typeof(AbpFeatureManagementApplicationContractsModule))]
    public class AbpFeatureManagementBlazorModule : AbpModule
    {

    }
}