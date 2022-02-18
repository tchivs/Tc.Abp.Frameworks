using FreeSql;

namespace Tchivs.Abp.FreeSql
{
    public class ConnectionData
    {
        public DataType DataType { get; set; }
        public string ConnectionString { get; set; }
        /// <summary>
        /// 【开发环境必备】自动同步实体结构到数据库，程序运行中检查实体表是否存在，然后创建或修改
        /// </summary>
        public bool AutoSyncStructure { get;   set; }
    }
}
