namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 日志组件实例(继承含行为操作) 接口
    /// </summary>
    public interface ILogger : ILog
    {
        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Trace"/>级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Debug"/>级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Info"/>级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Warn"/>级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Error"/>级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoggerLev.Fatal"/>级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }

    }
}
