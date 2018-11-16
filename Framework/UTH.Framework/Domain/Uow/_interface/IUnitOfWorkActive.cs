namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 工作单元接口(正在工作的工作单元)
    /// </summary>
    public interface IUnitOfWorkActive
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        dynamic Context { get; set; }

        /// <summary>
        /// 单元配置
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// 是否释放
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 完成事件
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// 失败事件
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 释放事件
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// 保存操作
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 保存操作(异步)
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

    }
}