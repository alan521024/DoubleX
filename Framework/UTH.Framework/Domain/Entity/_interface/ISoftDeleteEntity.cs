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
    /// 逻辑删除实体
    /// </summary>
    public interface ISoftDeleteEntity
    {
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        bool IsDelete { get; set; }
    }
}
