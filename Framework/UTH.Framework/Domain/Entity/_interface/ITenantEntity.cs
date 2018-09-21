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
    /// 是否租户信息
    /// </summary>
    public interface ITenantEntity<TKey>
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        TKey TenantId { get; set; }
    }

    /// <summary>
    /// 是否租户信息
    /// </summary>
    public interface ITenantEntity : ITenantEntity<Guid> { }
}
