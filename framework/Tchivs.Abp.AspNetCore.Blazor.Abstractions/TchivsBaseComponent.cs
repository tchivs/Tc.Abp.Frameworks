using BrowserInterop;
using BrowserInterop.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using Tchivs.Abp.AspNetCore.Blazor.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public abstract class TchivsBaseComponent : AbpComponentBase
    {
        [Inject, NotNull] public NavigationManager? Navigation { get; set; }
        [Inject, NotNull] public IJSRuntime? JsRuntime { get; set; }
        public WindowInterop? Window { get; private set; }
        public TchivsBaseComponent()
        {
            LocalizationResource = typeof(BlazorUIResource);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            Window ??= await JsRuntime.Window();
            if (Window != null&&firstRender)
            {
                await this.OnLoadAsync(Window);
            }
        }
        protected virtual Task OnLoadAsync(WindowInterop window)
        {
            return Task.CompletedTask;
        }
    }

}