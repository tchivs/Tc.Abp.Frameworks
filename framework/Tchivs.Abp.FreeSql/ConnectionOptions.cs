using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Tchivs.Abp.FreeSql
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class ConnectionOptions
    {
        /// <summary>
        /// 数据库命令执行前
        /// </summary>
        public Action<DbCommand> Executing { get; set; }
        /// <summary>
        /// 数据库命令执行后
        /// </summary>
        public Action<DbCommand, string> Executed { get; set; }
        public ConnectionData Default { get; set; } = new ConnectionData();
        public Dictionary<string, ConnectionData> Connections { get; set; }
        public ConnectionOptions()
        {
            Connections = new Dictionary<string, ConnectionData>() { { nameof(Default), Default } };
        }
        public void AddConnection(string name, ConnectionData connection)
        {
            this.Connections.Add(name, connection);
        }
        public ConnectionData GetConnection(string name = nameof(Default))
        {
            if (this.Connections.TryGetValue(name, out var conn))
            {
                return conn;
            }
            throw new NullReferenceException($"ConnectionOption：{name} is Empty! Please configure ConnectionOption");
        }
    }
}
