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
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkActive, IUnitOfWorkCompleteHandle, IDependency, IDisposable
    {
        /// <summary>
        /// 单元标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 开始事务
        /// </summary>
        void Begin(UnitOfWorkOptions options);




        /// <summary>
        /// 获取外部引用
        /// </summary>
        /// <returns></returns>
        IUnitOfWork GetOuter();
        /// <summary>
        /// 设置外部引用
        /// </summary>
        /// <param name=""></param>
        void SetOuter(IUnitOfWork outer);
    }
}