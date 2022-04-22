using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
namespace Tchivs.Abp.UI.Components
{
    partial class NavTabItem
    {
        public string Id { get =>$"v-pills-{Name.ToLower()}-tab";  }
        public string ContentId { get =>$"v-pills-{Name.ToLower()}"; }
        public string Selector { get =>$"#{ContentId}"; }
      [Parameter]  public string Name { get; set; }
        [Parameter] public bool Active { get; set; }
        public string Css { get=>  CssBuilder.Default("nav-link")
                .AddClass("active", this.Active)
                .Build();   }

        public  RenderFragment Content { get; set; }
        public string ContentCss
        {
            get => CssBuilder.Default("tab-pane fade")
            .AddClass("active show", this.Active)
            .Build();
        }
    }
}
