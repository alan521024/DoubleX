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
    /// 树结构实体接口
    /// </summary>
    public interface IEntityTree : IEntity
    {
        Guid Parent { get; set; }
        string Paths { get; set; }
        int Depth { get; set; }
        int Sort { get; set; }
    }
}
