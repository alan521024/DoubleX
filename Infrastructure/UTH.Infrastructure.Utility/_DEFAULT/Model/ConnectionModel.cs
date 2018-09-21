using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 连接信息
    /// </summary>
    public class ConnectionModel
    {
        public ConnectionModel() { }

        public ConnectionModel(string connectionString)
        {
            ConnectionString = connectionString;
        }


        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 库类型
        /// </summary>
        public EnumDbType DbType { get; set; }

        /// <summary>
        /// 库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Coding { get; set; } = "utf8";

        /// <summary>
        /// 驱动
        /// </summary>
        public string Provider { get; set; }


        /// <summary>
        /// 读地址
        /// </summary>
        public string ReadAddress { get; set; }

        /// <summary>
        /// 写地址
        /// </summary>
        public string WriteAddress { get; set; }


        /// <summary>
        /// 自动启动
        /// </summary>
        public bool AutoStart { get; set; } = true;

        /// <summary>
        /// 是否自动关闭连接
        /// </summary>
        public bool AutoCloseConnection { get; set; } = true;
        
        /// <summary>
        /// 连接队例长度
        /// </summary>
        public int PoolSize { get; set; }

        /// <summary>
        /// 超时时间(S/秒)
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 过期类型
        /// </summary>
        public EnumExpiresType ExpiresType { get; set; }

        /// <summary>
        /// 过期时间(S/秒)
        /// </summary>
        public long ExpireTime { get; set; }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return ConnectionString;
            }
            StringBuilder builder = new StringBuilder();
            switch (DbType)
            {
                case EnumDbType.SqlServer:
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", Address, DbName, User, Password);
                    break;
                case EnumDbType.MySql:
                    if (Port == 0) { Port = 3306; }
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};user id={2};password={3};port={4};Charset={5}", Address, DbName, User, Password, Port, Coding);
                    break;
                case EnumDbType.Oracle:
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};user id={2};password={3};persist security info=false;", Address, DbName, User, Password);
                    break;
                case EnumDbType.Sqlite:
                    builder.AppendFormat("Data Source={0};", Address);
                    break;
                case EnumDbType.Redis:
                    if (Port == 0) { Port = 6379; }
                    if (PoolSize == 0) { PoolSize = 60; }
                    if (string.IsNullOrWhiteSpace(DbName)) { DbName = "0"; }

                    string readStr = "", writeStr = "";
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        readStr = "";
                        writeStr = "";
                    }
                    else
                    {
                        readStr = string.Format("{0}@", Password);
                        writeStr = string.Format("{0}@", Password);
                    }

                    if (string.IsNullOrWhiteSpace(ReadAddress))
                    {
                        readStr = readStr + Address;
                    }
                    else
                    {
                        readStr = readStr + ReadAddress;
                    }

                    if (string.IsNullOrWhiteSpace(WriteAddress))
                    {
                        writeStr = writeStr + Address;
                    }
                    else
                    {
                        writeStr = writeStr + WriteAddress;
                    }

                    readStr = readStr + ":" + Port.ToString();
                    writeStr = writeStr + ":" + Port.ToString();

                    builder.AppendFormat("{0};{1};MaxWritePoolSize={2};MaxReadPoolSize={2};AutoStart={3};DefaultDb={4};",
                      readStr, writeStr, PoolSize, DbName);
                    break;
                case EnumDbType.Mongodb:
                    //builder.AppendFormat("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", Address, DBName, User, Password);
                    break;
            }
            return builder.ToString();
        }
    }
}
