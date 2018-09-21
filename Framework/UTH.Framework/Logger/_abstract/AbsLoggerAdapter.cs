namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 按名称缓存的日志实现适配器基类，用于创建并管理指定类型的日志实例
    /// </summary>
    public abstract class AbsLoggerAdapter : ILoggerAdapter
    {
        private readonly ConcurrentDictionary<string, ILogger> _cacheLoggers;
        
        /// <summary>
        /// 初始化一个<see cref="AbsLoggerAdapter"/>类型的新实例
        /// </summary>
        protected AbsLoggerAdapter()
        {
            _cacheLoggers = new ConcurrentDictionary<string, ILogger>();
        }
        
        /// <summary>
        /// 由指定类型获取<see cref="ILog"/>日志实例
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public  ILogger GetLogger(Type type)
        {
            type.CheckNull("type");
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 由指定名称获取<see cref="ILog"/>日志实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        public ILogger GetLogger(string name)
        {
            name.CheckNull("name");
            return GetLoggerInternal(name);
        }

        /// <summary>
        /// 创建指定名称的缓存实例(由具体实现，创建指定类型的日志对象)
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected abstract ILogger Create(string name);

        /// <summary>
        /// 清除缓存中的日志实例
        /// </summary>
        protected virtual void Clear()
        {
            _cacheLoggers.Clear();
        }

        /// <summary>
        /// 获取指定名称的Logger实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns>日志实例</returns>
        /// <exception cref="NotSupportedException">指定名称的日志缓存实例不存在则返回异常<see cref="NotSupportedException"/></exception>
        protected virtual ILogger GetLoggerInternal(string name)
        {
            ILogger log;
            if (_cacheLoggers.TryGetValue(name, out log))
            {
                return log;
            }
            log = Create(name);
            _cacheLoggers[name] = log ?? throw new NotSupportedException(string.Format(Lang.sysLoggerAdapterError, name, GetType().FullName));
            return log;
        }

    }
}