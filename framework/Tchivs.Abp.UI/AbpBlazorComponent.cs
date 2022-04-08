using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.AspNetCore.Components;

namespace Tchivs.Abp.UI
{
    public abstract class AbpBlazorComponent : AbpComponentBase
    {
        protected AbpBlazorComponent()
        {
            this.LocalizationResource = typeof(Localization.BlazorUIResource);
        }
        [Inject, NotNull] public NavigationManager Navigation { get; set; }
        [Inject, NotNull] public IJSRuntime JsRuntime { get; set; }
        public bool IsWebAssembly { get => OperatingSystem.IsBrowser(); }
        protected async Task NavigateToAsync(string uri, string target = null)
        {
            if (target == "_blank")
            {
                await JsRuntime.InvokeVoidAsync("open", uri, target);
            }
            else
            {
                Navigation.NavigateTo(uri);
            }
        }

    }
}
