using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Tchivs.Abp.Core;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Alerts;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using System.Linq;

namespace Tchivs.Abp.AspNetCore.Blazor;
[DependsOn(
        typeof(TchivsAbpAspNetCoreBlazorAbstractionsModule))]
public class TchivsAbpAspNetCoreBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddBootstrapBlazor();
        ConfigureRouter(context);
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(TchivsAbpAspNetCoreBlazorModule).Assembly;
            //  options.AdditionalAssemblies.Add(typeof(TchivsAbpAspNetCoreBlazorModule).Assembly);
        });
    }
}
public enum BlazorComponentType
{
    LanguageSwitch
}
public class BlazorComponentOption
{
    public Dictionary<BlazorComponentType, Type> Components { get; } = new Dictionary<BlazorComponentType, Type>();
    public void AddComponent(BlazorComponentType componentType, Type type)
    {
        if (Components.ContainsKey(componentType))
        {
            Components[componentType] = type;
        }
        else
        {
            Components.Add(componentType, type);
        }
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
                    await MessageService.Show(new MessageOption { Content = message.Text });
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

