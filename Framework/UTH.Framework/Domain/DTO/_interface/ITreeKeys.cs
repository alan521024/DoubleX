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
    public interface ITreeKeys<TKey>
    {
        TKey Id { get; set; }
        List<TKey> Ids { get; set; }
        TKey Parent { get; set; }
        string Paths { get; set; }
        int Depth { get; set; }
        int Sort { get; set; }
    }

    /// <summary>
    /// Key信息接口
    /// </summary>
    public interface ITreeKeys : ITreeKeys<Guid>
    {
    }
}
