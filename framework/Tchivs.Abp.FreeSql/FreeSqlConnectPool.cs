using FreeSql;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Tchivs.Abp.FreeSql
{
    internal class FreeSqlConnectPool : IFreeSqlConnectPool
    {
        private bool disposedValue;
        private readonly IOptions<ConnectionOptions> options;
        private readonly ILogger<FreeSqlConnectPool> logger;

        public ConcurrentDictionary<string, IFreeSql> Connects { get; set; } = new ConcurrentDictionary<string, IFreeSql>();
        public FreeSqlConnectPool(IOptions<ConnectionOptions> options, ILogger<FreeSqlConnectPool> logger)
        {
            this.options = options;
            this.logger = logger;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    foreach (var kv in this.Connects)
                    {
                        this.Connects.Remove(kv.Key, out var fsql);
                        fsql.Dispose();
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~FreeSqlConnectPool()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        public IFreeSql GetConnection()
        {
            return GetConnection(nameof(this.options.Value.Default));
        }
        public IFreeSql GetConnection(string name)
        {
            if (!this.Connects.TryGetValue(name, out var conn))
            {
                var connection = this.options.Value.GetConnection(name);
                logger.LogInformation($"create freesql connection\tName:{name}|DataType:{connection.DataType}|AutoSyncStructure:{connection.AutoSyncStructure}");
                var builder = new FreeSqlBuilder()
                    .UseConnectionString(connection.DataType, connection.ConnectionString)
                    .UseAutoSyncStructure(connection.AutoSyncStructure);
                if (this.options.Value.Executing != null)
                {
                    builder.UseMonitorCommand(this.options.Value.Executing, this.options.Value.Executed);
                }
                conn = builder.Build();
                if (!this.Connects.TryAdd(name, conn))
                {
                    throw new ApplicationException(nameof(GetConnection));
                }
            }
            return conn;
        }
    }
}
