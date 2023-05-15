using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Abp.AspNetCore.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Tc.Abp.AspNetCore.Components
{
    public class MenuBase : TcAbpComponentBase, IDisposable
    {
        [Inject]
        protected IMenuManager MenuManager { get; set; }
        protected ApplicationMenu Menu { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Menu = await MenuManager.GetAsync(StandardMenus.User);

            await base.OnInitializedAsync();
        }
    }
}
