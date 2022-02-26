using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.AspNetCore.Components.Alerts;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;
namespace Tchivs.Abp.AspNetCore.Blazor;
[Dependency(ReplaceServices = true)]
public class UiMessageService : IUiMessageService, IScopedDependency
{
    [Inject]
    [NotNull]
    public MessageService? MessageService { get; set; }
    public Task<bool> Confirm(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        throw new NotImplementedException();
    }
    async Task Show(Color color, string message, Action<UiMessageOptions> options = null)
    {
        await this.MessageService.Show(new MessageOption()
        {
            Content = message,
            Color = color,
        });
    }
    public Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        return this.Show(Color.Danger, message, options);
    }

    public Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        return this.Show(Color.Info, message, options);
    }

    public Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        return this.Show(Color.Success, message, options);
    }

    public Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        return this.Show(Color.Warning, message, options);
    }
}
[Dependency(ReplaceServices = true)]
public class AlertManager : IAlertManager, IScopedDependency
{
    public AlertList Alerts { get; set; } = new AlertList();
    public AlertManager()
    {
        this.Alerts.CollectionChanged += Alerts_CollectionChanged;
    }
    [Inject]
    [NotNull]
    public MessageService? MessageService { get; set; }
    Color GetColor(AlertType type)
    {
        return type switch
        {
            AlertType.Default => Color.Active,
            AlertType.Primary => Color.Primary,
            AlertType.Secondary => Color.Secondary,
            AlertType.Success => Color.Success,
            AlertType.Danger => Color.Danger,
            AlertType.Warning => Color.Warning,
            AlertType.Info => Color.Info,
            AlertType.Light => Color.Light,
            AlertType.Dark => Color.Dark,
            _ => Color.Info,
        };
    }
    private async void Alerts_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                if (e.NewItems != null && e.NewItems.Count > 0 && e.NewItems[0] is AlertMessage message)
                {
                    await MessageService.Show(new MessageOption { Content = message.Text, Color = GetColor(message.Type) });
                    Alerts.Remove(message);
                }
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                break;
            default:
                break;
        }
    }
}

