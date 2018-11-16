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
    /// 工作单元配置项
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        public ConnectionModel Connection { get; set; }
    }
}