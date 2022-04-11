using Volo.Abp.AspNetCore.Components;
using Volo.Abp.FeatureManagement.Localization;

namespace Tchivs.Abp.FeatureManagement.Blazor
{
    public abstract class AbpFeatureManagementComponentBase : AbpComponentBase
    {
        protected AbpFeatureManagementComponentBase()
        {
            LocalizationResource = typeof(AbpFeatureManagementResource);
        }
    }

}