using System;
using StackExchange.Redis;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Redis客户端信息
    /// </summary>
    public class RedisClient
    {
        #region 私有变量

        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// 连接对象
        /// </summary>
        private IConnectionMultiplexer _connMultiplexer;

        #endregion

        #region 公共属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get { return _connectionString; } }

        /// <summary>
        /// 默认连接库
        /// </summary>
        public int Database { get; set; }

        #endregion

        #region 构造方法

        public RedisClient(string connectionString, int database = 0)
        {
            _connectionString = connectionString;
            Database = database;
        }

        #endregion

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        public IConnectionMultiplexer GetConnection()
        {
            if ((_connMultiplexer == null) || !_connMultiplexer.IsConnected)
            {
                _connMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
                _connMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
                _connMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
                _connMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
                _connMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
                _connMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
                _connMultiplexer.InternalError += ConnMultiplexer_InternalError;
                _connMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
            }
            return _connMultiplexer;
        }

        /// <summary>
        /// 格式化Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefix"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public string FormatKey(string key, string prefix = "_", string formatStr = "{prefix}_{key}")
        {
            return formatStr.Replace("{prefix}", prefix).Replace("{key}", key);
        }

        #region 注册事件

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion

    }
}
