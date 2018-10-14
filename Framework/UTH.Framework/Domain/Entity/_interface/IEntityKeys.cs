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
    /// 实体接口
    /// </summary>
    public interface IEntityKeys<TKey> : IEntity<TKey>
    {
        List<TKey> Ids { get; set; }
    }

    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntityKeys : IEntityKeys<Guid>, IEntity<Guid>
    {
    }
}
