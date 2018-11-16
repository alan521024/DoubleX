namespace UTH.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using SqlSugar;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// SqlSugar 辅助类
    /// </summary>
    public static class SqlSugarHelper
    {
        public static SqlSugarClient GetContext(ConnectionModel connection)
        {
            var config = new ConnectionConfig()
            {
                ConnectionString = connection.GetConnectionString(),
                DbType = ToDBType(connection.DbType),
                IsAutoCloseConnection = connection.AutoCloseConnection,
                //IsShardSameThread = true //设为true相同线程是同一个SqlSugarClient http://www.codeisbug.com/Doc/8/1158
                //不能使用异步方法
            };

            var model = new SqlSugarClient(config);

            if (EngineHelper.Configuration.IsDebugger)
            {
                model.Ado.IsEnableLogEvent = true;
                model.Ado.LogEventStarting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" + model.Utilities.SerializeObject(pars.ToDictionary(s => s.ParameterName, s => s.Value)));
                    Console.WriteLine();
                };
            }

            return model;
        }

        public static OrderByType ConvertOrderType(string value)
        {
            if (value.IsEmpty())
                return OrderByType.Asc;
            value = value.ToLower().Trim();
            if (value == "desc" || value == "1")
                return OrderByType.Desc;

            return OrderByType.Asc;
        }

        public static SqlSugar.DbType ToDBType(EnumDbType dbType)
        {
            switch (dbType)
            {
                case EnumDbType.SqlServer:
                    return SqlSugar.DbType.SqlServer;
                case EnumDbType.MySql:
                    return SqlSugar.DbType.MySql;
                case EnumDbType.Oracle:
                    return SqlSugar.DbType.Oracle;
                case EnumDbType.Sqlite:
                    return SqlSugar.DbType.Sqlite;
                default:
                    return SqlSugar.DbType.SqlServer;
            }
        }

        public static EnumDbType ResDBType(SqlSugar.DbType dbType)
        {
            switch (dbType)
            {
                case SqlSugar.DbType.SqlServer:
                    return EnumDbType.SqlServer;
                case SqlSugar.DbType.MySql:
                    return EnumDbType.MySql;
                case SqlSugar.DbType.Oracle:
                    return EnumDbType.Oracle;
                case SqlSugar.DbType.Sqlite:
                    return EnumDbType.Sqlite;
                default:
                    return EnumDbType.SqlServer;
            }
        }
    }
}
