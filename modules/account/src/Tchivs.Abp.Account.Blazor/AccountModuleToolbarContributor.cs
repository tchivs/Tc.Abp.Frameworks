using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.AspNetCore.Blazor.Abstractions;
using Volo.Abp.Users;

public class AccountModuleToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars.StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        if (!context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
        {
            //context.Toolbar.Items.Add(new ToolbarItem(typeof(UserLoginLinkViewComponent)));
        }

        return Task.CompletedTask;
    }
}
