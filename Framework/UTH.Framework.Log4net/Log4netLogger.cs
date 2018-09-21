namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using log4net.Core;
    using ILogger = log4net.Core.ILogger;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Log4net 日志记录者
    /// </summary>
    public class Log4netLogger : AbsLogger
    {
        private static readonly Type DeclaringType = typeof(Log4netLogger);
        private readonly log4net.Core.ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="Log4NetLog"/>类型的新实例
        /// </summary>
        public Log4netLogger(ILoggerWrapper wrapper)
        {
            _logger = wrapper.Logger;
        }

        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        /// <param name="isData">是否数据日志</param>
        protected override void Write(EnumLoggerLev level, object message, Exception exception = null)
        {
            Level log4netLevel = GetLog4netLevel(level);
            if (!TypeHelper.IsPrimitiveExtendedIncludingNullable(message.GetType()))
            {
                message = JsonHelper.Serialize(message);
            }
            _logger.Log(DeclaringType, log4netLevel, message, exception);
        }

        #region Override Logger Attribute

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return _logger.IsEnabledFor(Level.Trace); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsEnabledFor(Level.Debug); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return _logger.IsEnabledFor(Level.Info); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return _logger.IsEnabledFor(Level.Warn); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return _logger.IsEnabledFor(Level.Error); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return _logger.IsEnabledFor(Level.Fatal); }
        }

        #endregion

        #region 辅助操作

        /// <summary>
        /// 获取日志输出级别
        /// </summary>
        /// <param name="level">日志输出级别枚举</param>
        /// <returns>获取日志输出级别</returns>
        private static Level GetLog4netLevel(EnumLoggerLev level)
        {
            switch (level)
            {
                case EnumLoggerLev.All:
                    return Level.All;
                case EnumLoggerLev.Trace:
                    return Level.Trace;
                case EnumLoggerLev.Debug:
                    return Level.Debug;
                case EnumLoggerLev.Info:
                    return Level.Info;
                case EnumLoggerLev.Warn:
                    return Level.Warn;
                case EnumLoggerLev.Error:
                    return Level.Error;
                case EnumLoggerLev.Fatal:
                    return Level.Fatal;
                case EnumLoggerLev.Off:
                    return Level.Off;
                default:
                    return Level.Off;
            }
        }


        #endregion
    }
}