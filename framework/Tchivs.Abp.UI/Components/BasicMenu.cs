using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Tchivs.Abp.UI.Components
{
    public abstract class BasicMenuComponent : AbpBlazorComponent
    {
        [Inject]
        public IMenuManager MenuManager { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var mainMenu = await MenuManager.GetMainMenuAsync();
            await ProcessMenuItems(mainMenu.Items);
        }

        protected abstract Task ProcessMenuItems(ApplicationMenuItemList items);
    }
}
