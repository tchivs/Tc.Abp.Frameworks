using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Tchivs.Abp.UI.Components
{
    partial class NavTabs
{
       [Parameter] public RenderFragment Items { get; set; }
        [Parameter] public RenderFragment Content { get; set; }

    }
}
