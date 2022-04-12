using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
        FeatureManagement.Blazor.Components.FeatureManagementModal featureManagementModal;
        protected const string FeatureProviderName = "T";

        protected bool HasManageFeaturesPermission;
        protected string ManageFeaturesPolicyName;

        public TenantManagement()
        {
            LocalizationResource = typeof(AbpTenantManagementResource);
            ObjectMapperContext = typeof(AbpTenantManagementBlazorModule);
        
            ManageFeaturesPolicyName = TenantManagementPermissions.Tenants.ManageFeatures;

        }
        EventCallback<MouseEventArgs> ClickManageFeaturesCallback(TenantDto tenant) =>
     EventCallback.Factory.Create<MouseEventArgs>(this, () => ShowManageFeatures(tenant));

        private async Task ShowManageFeatures(TenantDto tenant)
        {
            await featureManagementModal.OpenAsync(FeatureProviderName, tenant.Id.ToString());
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
