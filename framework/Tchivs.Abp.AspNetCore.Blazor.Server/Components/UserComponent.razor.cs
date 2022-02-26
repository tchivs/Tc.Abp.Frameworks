using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Volo.Abp.UI.Navigation;


namespace Tchivs.Abp.AspNetCore.Blazor.Components
{
    public partial class UserComponent 
    {
        [Inject, NotNull]
        protected IMenuManager? MenuManager { get; set; }


        protected ApplicationMenu? Menu { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (MenuManager != null)
            {

                Menu = await MenuManager.GetAsync(StandardMenus.User);
            }
            Navigation.LocationChanged += OnLocationChanged;
        }

        protected virtual void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }

    }
}
