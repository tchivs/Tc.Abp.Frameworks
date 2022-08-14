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
using Microsoft.JSInterop;
using Tchivs.Abp.UI.Toolbars;
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
        [Inject] [NotNull] private ICurrentUser CurrentUser { get; set; }
        [Inject] [NotNull] private NavigationManager NavigationManager { get; set; }
        [Inject] [NotNull] IMenuManager MenuManager { get; set; }
        [Inject] [NotNull] private IBrandingProvider BrandingProvider { get; set; }
        [Inject] [NotNull] private ToastService ToastService { get; set; }
        [Inject]
        [NotNull]
        private NavigationManager Navigation { get; set; }
        [Inject] [NotNull] private IJSRuntime JsRuntime { get; set; }
        public string Url { get; set; }
        public string TabDefaultUrl { get; set; } = "/Index";
        private List<RenderFragment> RightToolbarItemRenders { get; set; } = new();
        private List<RenderFragment> LeftToolbarItemRenders { get; set; } = new();
        private async Task<bool> OnAuthorizing(string url)
        {
            return true;
        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            bool isWebAssembly = JsRuntime is IJSInProcessRuntime;
            var url = Uri.EscapeDataString(Navigation.Uri);
            Url = isWebAssembly ? $"authentication/login?returnUrl={url}" : "Account/Login";
            var mainMenu = await MenuManager.GetMainMenuAsync();
            MenuItems = GetIconSideMenuItems(mainMenu.Items);
            await CreateToobar(LeftToolbarItemRenders, StandardToolbars.Left);
            await CreateToobar(RightToolbarItemRenders, StandardToolbars.Right);
        }
        async Task CreateToobar(List<RenderFragment> renders, string name)
        {
            var toolbar = await this._toolbarManager.GetAsync(name);
            renders.Clear();
            var sequence = 0;
            foreach (var item in toolbar.Items)
            {
                renders.Add(builder =>
                {
                    builder.OpenComponent(sequence++, item.ComponentType);
                    builder.CloseComponent();
                });
            }
        }

        private Task DrawerSwitch()
        {
            return Task.CompletedTask;
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
        }

        /// <summary>
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