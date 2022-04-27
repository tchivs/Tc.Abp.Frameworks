using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.UI.Bootstrap.WebAssembly.Components;
using Tchivs.Abp.UI.Toolbars;

namespace Tchivs.Abp.UI.Bootstrap.WebAssembly
{
    public class ToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name == StandardToolbars.Right)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));

                //TODO: Can we find a different way to understand if authentication was configured or not?
                var authenticationStateProvider = context.ServiceProvider
                    .GetService<AuthenticationStateProvider>();

                if (authenticationStateProvider != null)
                {
                    context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
                }
            }

            return Task.CompletedTask;
        }


    }
}