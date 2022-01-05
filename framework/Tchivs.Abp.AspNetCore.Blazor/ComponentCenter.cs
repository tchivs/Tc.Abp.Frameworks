using Microsoft.AspNetCore.Components;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tchivs.Abp.AspNetCore.Blazor;

public class ComponentCenter : ISingletonDependency
{
    private readonly BlazorComponentOption options;

    public ComponentCenter(IOptions<BlazorComponentOption> options)
    {
        this.options = options.Value;
    }
    public RenderFragment Render(BlazorComponentType componentType)
    {
        var type = this.options.Components[componentType];
        if (type == null)
        {
            throw new NullReferenceException($"BlazorComponentType {componentType} is not register!");
        }
        return new RenderFragment(builder =>
        {
            builder.OpenComponent(0, type);
            builder.CloseComponent();
        });
    }
}

