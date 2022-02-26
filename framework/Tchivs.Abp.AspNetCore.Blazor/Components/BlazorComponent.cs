using System.Diagnostics.CodeAnalysis;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace Tchivs.Abp.AspNetCore.Blazor.Components
{
    public abstract class BlazorComponent : Abstractions.TchivsBaseComponent
    {
        [Inject]
        [NotNull]
        protected DialogService? DialogService { get; set; }
        [Inject]
        [NotNull]
        protected ToastService? Toast { get; set; }
        protected override async Task HandleErrorAsync(Exception exception)
        {
           await this.Message.Error(exception.Message);
        }

    }
}
