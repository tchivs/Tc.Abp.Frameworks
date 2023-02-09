using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.AspNetCore.PageToolbars;

public class PageToolbarManager : IPageToolbarManager, ITransientDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public PageToolbarManager(
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
    }

    public virtual async Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar)
    {
        if (toolbar == null || !toolbar.Contributors.Any())
        {
            return Array.Empty<PageToolbarItem>();
        }

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var context = new PageToolbarContributionContext(scope.ServiceProvider);

            foreach (var contributor in toolbar.Contributors)
            {
                await contributor.ContributeAsync(context);
            }

            return context.Items.OrderBy(i => i.Order).ToArray();
        }
    }
}
