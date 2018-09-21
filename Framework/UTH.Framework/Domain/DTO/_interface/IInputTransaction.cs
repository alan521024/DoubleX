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
    /// 事务参数
    /// </summary>
    public interface IInputTransaction
    {
        /// <summary>
        /// 是否外部事务
        /// </summary>
        bool IsTransaction { get; set; }
    }
}
