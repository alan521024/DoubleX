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
    /// Key信息接口
    /// </summary>
    public interface IKeys<TKey>
    {
        TKey Id { get; set; }

        List<TKey> Ids { get; set; }
    }

    /// <summary>
    /// Key信息接口
    /// </summary>
    public interface IKeys : IKeys<Guid>
    {
    }
}
