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
    /// 基础信息（Key + Audited + Tenant + SoftDeleteEntity）
    /// </summary>
    public class BaseFullEntity<TKey> : IEntity<TKey>, IAuditedEntity<TKey>, ITenantEntity<TKey>, ISoftDeleteEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public TKey TenantId { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public TKey CreateId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDt { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public TKey LastId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastDt { get; set; }


        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDelete { get; set; }
    }

    /// <summary>
    /// 基础信息（Key + Audited + Tenant + SoftDeleteEntity）
    /// </summary>
    public class BaseFullEntity : BaseFullEntity<Guid>, IEntity, IAuditedEntity, ITenantEntity, ISoftDeleteEntity
    {

    }
}
