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
    /// 基础信息（Key）
    /// </summary>
    public class BaseEntity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 基础信息（Key）
    /// </summary>
    public class BaseEntity : BaseEntity<Guid>, IEntity
    {
    }

}
