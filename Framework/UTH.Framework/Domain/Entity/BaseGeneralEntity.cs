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
    using SqlSugar;

    /// <summary>
    /// 基础信息（Key + Audited + SoftDeleteEntity）
    /// </summary>
    public class BaseGeneralEntity<TKey> : ModelContext, IEntity<TKey>, IAuditedEntity<TKey>, ISoftDeleteEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }

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
    /// 基础信息（Key + Audited + SoftDeleteEntity）
    /// </summary>
    public class BaseGeneralEntity : BaseGeneralEntity<Guid>, IEntity, IAuditedEntity, ISoftDeleteEntity
    {

    }
}
