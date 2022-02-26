using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public class ToolbarItem
    {
        public Type ComponentType
        {
            get;
        }
      

        public int Order { get; set; }

        public ToolbarItem([NotNull] Type componentType, int order = 0)
        {
            Order = order;
            ComponentType = Check.NotNull(componentType, nameof(componentType));
        }
    }
}