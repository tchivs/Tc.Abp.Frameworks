using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using Volo.Abp.Ui.Branding;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Tchivs.Abp.UI.Layouts
{
    partial class MainLayout : IDisposable
    {
        private IEnumerable<MenuItem> MenuItems { get; set; }
        private string Title { get; set; }
        private string Footer { get; set; }
        [Inject]
        [NotNull]
        private ICurrentUser CurrentUser { get; set; }
        [Inject]
        [NotNull]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        [NotNull] IMenuManager MenuManager { get; set; }
        [Inject]
        [NotNull]
        private IBrandingProvider BrandingProvider { get; set; }
        [Inject]
        [NotNull]
        private ToastService ToastService { get; set; }
        private async Task<bool> OnAuthorizing(string url)
        {
            return true;
        }
        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var mainMenu = await MenuManager.GetMainMenuAsync();
            MenuItems = GetIconSideMenuItems(mainMenu.Items);
        }
        private static List<MenuItem> GetIconSideMenuItems(ApplicationMenuItemList items)
        {
            var menus = new List<MenuItem>();
            foreach (var item in items)
            {
                var menu = new MenuItem()
                {
                    Text = item.DisplayName,
                    Icon = item.Icon,
                    Url = item.Url,
                };
                if (menu.Url == "/")
                {
                    menu.Match = NavLinkMatch.All;
                }

                menus.Add(menu);
                menu.Items = GetIconSideMenuItems(item.Items);
            }
            return menus;
        }
        private async Task OnErrorHandleAsync(ILogger logger, Exception ex)
        {
            await ToastService.Error(Title, ex.Message);

            logger.LogError(ex, "ErrorLogger");
        }

        bool ShowFooter()
        {
            return !this.Footer.IsNullOrEmpty();
        }
        /// <summary>
        /// 
        /// </summary>
        public Task OnUpdateAsync(string key)
        {
            if (key == "title")
            {
               // Title = DictsService.GetWebTitle();
            }
            else if (key == "footer")
            {
               // Footer = DictsService.GetWebFooter();
            }
            StateHasChanged();
            return Task.CompletedTask;
        }

        private void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
            }
        }    /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
