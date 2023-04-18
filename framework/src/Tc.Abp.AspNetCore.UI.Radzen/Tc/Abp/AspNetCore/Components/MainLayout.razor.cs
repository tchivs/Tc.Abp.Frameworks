﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.AspNetCore.UI;

namespace Tc.Abp.AspNetCore.Components;
public partial class MainLayout
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    private bool IsCollapseShown { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private void ToggleCollapse()
    {
        IsCollapseShown = !IsCollapseShown;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        IsCollapseShown = false;
        InvokeAsync(StateHasChanged);
    }
    RadzenSidebar sidebar0;
    RadzenBody body0;
    bool sidebarExpanded = true;
    bool bodyExpanded = false;




    void FilterPanelMenu(ChangeEventArgs args)
    {
        var term = args.Value.ToString();
        //examples = string.IsNullOrEmpty(term) ? ExampleService.Examples : ExampleService.Filter(term);
    }


    async Task PanelMenuClick(MenuItemEventArgs args)
    {
        if (args.Path == "/")
        {
            await JSRuntime.InvokeVoidAsync("document.location.reload");
        }
    }
}
