using System;
using Volo.Abp.DependencyInjection;

namespace Tchivs.Abp.FreeSql
{
  internal  interface IFreeSqlConnectPool : ISingletonDependency, IDisposable
    {
        IFreeSql GetConnection();
        IFreeSql GetConnection(string name);
    }
}
