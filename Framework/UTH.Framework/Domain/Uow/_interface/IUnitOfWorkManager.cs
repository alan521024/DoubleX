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
    /// 工作单元管理
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前活动的工作单元(如果不存在，则为空)。
        /// </summary>
        IUnitOfWorkActive Current { get; }

        /// <summary>
        /// 开始单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options = null);
    }
}
