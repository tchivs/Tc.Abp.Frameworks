﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace Tchivs.Abp.UI.Components
{
    public abstract class AbpBootstrapBlazorComponent : AbpBlazorComponent
    {
        
        [Inject]
        [NotNull]
        protected DialogService DialogService { get; set; }
        [Inject]
        [NotNull]
        protected ToastService Toast { get; set; }
        protected override async Task HandleErrorAsync(Exception exception)
        {
           await this.Message.Error(exception.Message);
        }

    }
}
