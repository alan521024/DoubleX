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
    /// 日志组件 行为操作接口
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Trace<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Trace(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Debug<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Debug(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Info<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Warn<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Warn(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Error<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Fatal<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, params object[] args);

    }
}
