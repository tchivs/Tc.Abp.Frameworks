﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.UI.Components
{
    [CascadingTypeParameter(nameof(TItem))]
    public class ReadOnlyBaseTable<TAppService, TItem, TKey, TGetListInput> : AbpBootstrapBlazorComponent
             where TAppService : IReadOnlyAppService<TItem, TItem, TKey, TGetListInput>
        where TItem : class, IEntityDto<TKey>, new()
        where TGetListInput : PagedAndSortedResultRequestDto, new()
    {
        protected virtual IEnumerable<int> PageItemsSource => new int[] { 10, 20, 50 };
        [Inject][NotNull] protected TAppService AppService { get; set; }
        protected virtual async Task<QueryData<TItem>> OnQueryAsync(QueryPageOptions option)
        {
            var result = await this.AppService.GetListAsync(Convert2Input(option));
            return new QueryData<TItem>()
            {
                TotalCount = (int)result.TotalCount,
                Items = result.Items
            };
        }
        protected virtual TGetListInput Convert2Input(QueryPageOptions options)
        {
            return new TGetListInput()
            {
                MaxResultCount = options.PageItems,
                SkipCount = options.PageIndex == 1 ? 0 : options.PageIndex * options.PageItems
            };
        }
    }
}
