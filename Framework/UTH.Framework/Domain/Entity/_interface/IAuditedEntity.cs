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
    /// 审计实体(添加自动为当前用户，修改自动为当前用户)
    /// </summary>
    public interface IAuditedEntity 
    {
        /// <summary>
        /// 添加人Id 
        /// </summary>
        Guid CreateId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        DateTime CreateDt { get; set; }

        /// <summary>
        /// 修改人Id 
        /// </summary>
        Guid LastId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime LastDt { get; set; }
    }
}
