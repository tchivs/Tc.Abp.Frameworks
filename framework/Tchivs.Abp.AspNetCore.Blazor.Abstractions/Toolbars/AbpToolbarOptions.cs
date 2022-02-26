using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public class AbpToolbarOptions
    {
        [NotNull]
        public List<IToolbarContributor> Contributors { get; }

        public AbpToolbarOptions()
        {
            Contributors = new List<IToolbarContributor>();
        }
    }
}
