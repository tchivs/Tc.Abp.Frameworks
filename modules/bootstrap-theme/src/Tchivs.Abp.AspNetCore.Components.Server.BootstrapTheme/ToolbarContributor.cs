
using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme.Components;
using Tchivs.Abp.UI.Toolbars;

namespace Tchivs.Abp.AspNetCore.Components.Server.BootstrapTheme
{
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

}