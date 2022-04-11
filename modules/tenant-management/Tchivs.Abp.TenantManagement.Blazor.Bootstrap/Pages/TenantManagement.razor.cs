using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Localization;

namespace Tchivs.Abp.TenantManagement.Blazor.Pages
{
    partial class TenantManagement
    {
        protected const string FeatureProviderName = "T";

        protected bool HasManageFeaturesPermission;
        protected string ManageFeaturesPolicyName;
        public TenantManagement()
        {
            LocalizationResource = typeof(AbpTenantManagementResource);
            ObjectMapperContext = typeof(AbpTenantManagementBlazorModule);
        
            ManageFeaturesPolicyName = TenantManagementPermissions.Tenants.ManageFeatures;

        }
        protected override async Task OnInitializedAsync()
        {
            HasManageFeaturesPermission = await AuthorizationService.IsGrantedAsync(ManageFeaturesPolicyName);

            await base.OnInitializedAsync();
        }
      
        //protected override string GetDeleteConfirmationMessage(TenantDto entity)
        //{
        //    return string.Format(L["TenantDeletionConfirmationMessage"], entity.Name);
        //}

    }
}
