using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Tchivs.Abp.UI.Bootstrap.Components
{
    public abstract class BaseLoginDisplay : AbpBlazorComponent
    {
        [Inject, NotNull]
        protected IMenuManager MenuManager { get; set; }


        protected ApplicationMenu Menu { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Menu = await MenuManager.GetAsync(StandardMenus.User);
            Navigation.LocationChanged += OnLocationChanged;
        }

        protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        protected virtual void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }
        protected abstract Task Logout();
        protected abstract Task Login();
    }
}
