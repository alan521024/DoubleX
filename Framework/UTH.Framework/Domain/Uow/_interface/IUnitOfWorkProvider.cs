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
    /// 用于获取/设置当前 工作单元
    /// </summary>
    public interface IUnitOfWorkProvider
    {
        /// <summary>
        /// 当前工作单元
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}
