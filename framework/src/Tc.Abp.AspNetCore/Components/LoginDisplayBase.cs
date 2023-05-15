using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace Tc.Abp.AspNetCore.Components;
    public abstract class LoginDisplayBase : MenuBase
{
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Navigation.LocationChanged += OnLocationChanged;
        }

        protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        public virtual void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }
    }
