using System.Collections.Generic;
using System.Reflection;

namespace Tchivs.Abp.UI
{
    public class AbpRouterOptions
    {
        public Assembly AppAssembly { get; set; }
        public Type DefaultLayout { get; set; }
        public List<Assembly> AdditionalAssemblies { get; }

        public AbpRouterOptions()
        {
            AdditionalAssemblies = new List<Assembly>();
        }
    }
}