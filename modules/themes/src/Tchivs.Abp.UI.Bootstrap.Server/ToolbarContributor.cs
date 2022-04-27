using Tchivs.Abp.UI.Bootstrap.Server.Components;
using Tchivs.Abp.UI.Toolbars;

namespace Tchivs.Abp.UI.Bootstrap.Server;

public class ToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Right)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
        }
        return Task.CompletedTask;
    }
}