using Volo.Abp.DependencyInjection;

namespace Tchivs.Abp.FreeSql
{
    public interface IFreeSqlSelector : ISingletonDependency
    {
        IFreeSql GetFreeSql(string name);
        IFreeSql GetFreeSql();
    }
}
