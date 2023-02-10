using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.AspNetCore.Components;
using JetBrains.Annotations;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Users;
using Microsoft.Extensions.Configuration;
using Fluxor;
using Fluxor.UnsupportedClasses;

namespace Tc.Abp.AspNetCore;

public abstract class TcAbpComponentBase : AbpComponentBase
{
    protected TcAbpComponentBase()
    {
        this.LocalizationResource = typeof(Localization.BlazorResource);
        StateHasChangedThrottler = new ThrottledInvoker(() =>
        {
            if (!IsDisposed)
                InvokeAsync(StateHasChanged);
        });

    }
    public IConfiguration Configuration => LazyGetRequiredService(ref _configuration);
    IConfiguration _configuration;
    private IDisposable StateSubscription;
    private readonly ThrottledInvoker StateHasChangedThrottler;
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject]public IJSRuntime JsRuntime { get; set; }
    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }
    /// <summary>
    /// If greater than 0, the feature will not execute state changes
    /// more often than this many times per second. Additional notifications
    /// will be surpressed, and observers will be notified of the latest
    /// state when the time window has elapsed to allow another notification.
    /// </summary>
    protected byte MaximumStateChangedNotificationsPerSecond { get; set; }
    public bool IsWebAssembly { get => OperatingSystem.IsBrowser(); }
    /// <see cref="IActionSubscriber.SubscribeToAction{TAction}(object, Action{TAction})"/>
    public void SubscribeToAction<TAction>(Action<TAction> callback)
    {
        ActionSubscriber.SubscribeToAction<TAction>(this, action =>
        {
            InvokeAsync(() =>
            {
                if (!IsDisposed)
                    callback(action);
                StateHasChanged();
            });
        });
    }
    protected virtual async Task NavigateToAsync(string uri, string target = null)
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
    string GetBaseRedirectUrl()
    {
        if (IsWebAssembly)
        {
            return $"authentication/login";
        }
        else
        {
            return $"account/login";
        }
    }
    protected virtual string GetRedirectUrl()
    {
        var baseUrl = GetBaseRedirectUrl();
        var selfUrl = Configuration["App:SelfUrl"];
        if (string.IsNullOrEmpty(selfUrl))
        {
            return baseUrl;

        }
        string returnUrl = Uri.EscapeDataString(Navigation.Uri);
        if (returnUrl == selfUrl)
        {
            return baseUrl;
        }
        return $"{baseUrl}?returnUrl={returnUrl}";
    }
    protected virtual void RedirectToLogin()
    {
        string url = this.GetRedirectUrl();
        if (IsWebAssembly)
        {
            Navigation.NavigateTo(url);
        }
        else
        {
            Navigation.NavigateTo(url, true);
        }
    }
    /// <summary>
    /// Subscribes to state properties
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        StateSubscription = StateSubscriber.Subscribe(this, _ =>
        {
            StateHasChangedThrottler.Invoke(MaximumStateChangedNotificationsPerSecond);
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (StateSubscription is null)
                this.HandleErrorAsync(new NullReferenceException("Have you forgotten to call base.OnInitialized() in your component?"));

            StateSubscription.Dispose();
            ActionSubscriber?.UnsubscribeFromAllActions(this);
            GC.SuppressFinalize(this);
        }
    }

}

