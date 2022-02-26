using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Tchivs.Abp.Core;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using System.Linq;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Localization;
namespace Tchivs.Abp.AspNetCore.Blazor;
[DependsOn(
        typeof(TchivsAbpAspNetCoreBlazorAbstractionsModule))]
public class TchivsAbpAspNetCoreBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorizationCore();
        context.Services.AddBootstrapBlazor();
        ConfigureRouter(context);
    }
    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(TchivsAbpAspNetCoreBlazorModule).Assembly;
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

