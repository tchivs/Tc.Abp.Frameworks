﻿using System.Reflection;

namespace Tc.Abp.AspNetCore;

public class AbpRouterOptions
{
    public Assembly AppAssembly { get=>AppType.Assembly;   }
    public Type AppType { get; set; }
    /// <summary>
    /// for server
    /// </summary>
    public Type HeaderStaticComponent { get; set; }
    public Type DefaultLayout { get; set; }
    public Type NotFoundLayout { get; set; }
    public List<Assembly> AdditionalAssemblies { get; }
    public AbpRouterOptions()
    {
        AdditionalAssemblies = new List<Assembly>();
    }
}
