using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace Tchivs.Abp.UI.Components
{

    public class CrudBaseTable<TAppService, TItem, TKey, TGetListInput, TCreateInput,
        TUpdateInput> : ReadOnlyBaseTable<TAppService, TItem, TKey, TGetListInput>
        where TAppService : ICrudAppService<TItem, TKey, TGetListInput, TCreateInput,
            TUpdateInput>, IReadOnlyAppService<TItem, TItem, TKey, TGetListInput>
        where TItem : class, IEntityDto<TKey>, new()
        where TCreateInput : class, new()
        where TUpdateInput : class, new()
        where TGetListInput : PagedAndSortedResultRequestDto, new()
    {
        #region properties
        /// <summary>
        /// 更新或删除混合模板 如果设置了值则CreateTemplate&UpdateTemplate不启用
        /// </summary>
        [Parameter] public RenderFragment<AddOrUpdateContext<TKey, TItem, TCreateInput, TUpdateInput>> EditTemplate { get; set; }

        /// <summary>
        /// 新增时模板
        /// </summary>
        [Parameter] public RenderFragment<TCreateInput> CreateTemplate { get; set; }
        /// <summary>
        /// 编辑时模板
        /// </summary>
        [Parameter] public RenderFragment<TUpdateInput> UpdateTemplate { get; set; }
        /// <summary>
        /// 创建权限名
        /// </summary>
        [Parameter] public string CreatePolicyName { get; set; }
        /// <summary>
        /// 更新权限名
        /// </summary>
        [Parameter] public string UpdatePolicyName { get; set; }
        /// <summary>
        /// 删除权限名
        /// </summary>
        [Parameter] public string DeletePolicyName { get; set; }
        protected bool HasCreatePermission { get; set; }
        protected bool HasUpdatePermission { get; set; }
        protected bool HasDeletePermission { get; set; }

        #endregion
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await InvokeAsync(StateHasChanged);
        }
        protected virtual async Task SetPermissionsAsync()
        {
            if (CreatePolicyName != null)
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
            }

            if (UpdatePolicyName != null)
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
            }

            if (DeletePolicyName != null)
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
            }
        }
        protected virtual async Task<bool> OnSaveAsync(TItem model, ItemChangedType type)
        {
            bool result = false;
            try
            {
                if (type == ItemChangedType.Add)
                {
                    await AppService.CreateAsync(ObjectMapper.Map<TItem, TCreateInput>(model));
                }
                else
                {
                    await AppService.UpdateAsync(model.Id, ObjectMapper.Map<TItem, TUpdateInput>(model));
                }

                result = true;
            }
            catch (Exception e)
            {
                await this.HandleErrorAsync(e);
                result = false;
            }

            return result;
        }
        protected virtual Task<bool> OnDeleteAsync(IEnumerable<TItem> items)
        {
            return OnDeleteAsync(items.ToArray());
        }
        protected virtual async Task<bool> OnDeleteAsync(params TItem[] items)
        {
            bool success = false;
            try
            {
                foreach (var item in items)
                {
                    await this.AppService.DeleteAsync(item.Id);
                }
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                await HandleErrorAsync(e);
            }

            return success;
        }
    }
}
