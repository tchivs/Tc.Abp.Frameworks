namespace Tchivs.Abp.FreeSql
{
    internal  class FreeSqlSelector : IFreeSqlSelector
    {
        private readonly IFreeSqlConnectPool connectPool;

        public FreeSqlSelector(IFreeSqlConnectPool connectPool)
        {

            this.connectPool = connectPool;
        }
        public IFreeSql GetFreeSql()
        {
            return connectPool.GetConnection();
        }
        public IFreeSql GetFreeSql(string name)
        {
            return connectPool.GetConnection(name);
        }
    }
}
