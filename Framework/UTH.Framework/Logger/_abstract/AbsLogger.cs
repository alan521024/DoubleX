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
    /// 日志默认实例(基类)
    /// 日志输出者适配基类，用于定义日志输出的处理业务
    /// </summary>
    public abstract class AbsLogger : ILogger
    {
        #region 日志对象属性

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Trace"/>级别的日志
        /// </summary>
        public abstract bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Debug"/>级别的日志
        /// </summary>
        public abstract bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Info"/>级别的日志
        /// </summary>
        public abstract bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Warn"/>级别的日志
        /// </summary>
        public abstract bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Error"/>级别的日志
        /// </summary>
        public abstract bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="EnumLoggerLev.Fatal"/>级别的日志
        /// </summary>
        public abstract bool IsFatalEnabled { get; }

        #endregion

        #region 日志对象操作

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Trace"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public virtual void Trace<T>(T message)
        {
            if (IsTraceEnabled)
            {
                Write(EnumLoggerLev.Trace, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Trace(string format, params object[] args)
        {
            if (IsTraceEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Trace, format);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Debug"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public virtual void Debug<T>(T message)
        {
            if (IsDebugEnabled)
            {
                Write(EnumLoggerLev.Debug, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Debug(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Debug, format);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Info"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="isData">是否数据日志</param>
        public virtual void Info<T>(T message)
        {
            if (IsInfoEnabled)
            {
                Write(EnumLoggerLev.Info, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Info(string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Info, format);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Warn"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public virtual void Warn<T>(T message)
        {
            if (IsWarnEnabled)
            {
                Write(EnumLoggerLev.Warn, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Warn(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Warn, format);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Error"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public virtual void Error<T>(T message)
        {
            if (IsErrorEnabled)
            {
                Write(EnumLoggerLev.Error, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Error, format);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Fatal"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public virtual void Fatal<T>(T message)
        {
            if (IsFatalEnabled)
            {
                Write(EnumLoggerLev.Fatal, message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoggerLev.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, params object[] args)
        {
            if (IsFatalEnabled)
            {
                if (format.Contains("{") && format.Contains("}") && args.Length > 0)
                {
                    format = string.Format(format, args);
                }
                Write(EnumLoggerLev.Fatal, format);
            }
        }

        #endregion

        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        protected abstract void Write(EnumLoggerLev level, object message, Exception exception = null);
    }
}