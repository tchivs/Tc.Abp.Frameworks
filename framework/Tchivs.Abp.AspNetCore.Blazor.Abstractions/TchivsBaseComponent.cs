using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.AspNetCore.Components;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public abstract class TchivsBaseComponent : AbpComponentBase
    {
        [Inject, NotNull] public NavigationManager? Navigation { get; set; }
        [Inject, NotNull] public IJSRuntime? JsRuntime { get; set; }
    }
}