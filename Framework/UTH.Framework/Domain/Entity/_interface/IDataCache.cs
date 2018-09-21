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
    /// 是否数据缓存
    /// </summary>
    public interface IDataCache
    {
        /// <summary>
        /// 是否数据缓存
        /// </summary>
        bool IsDataCache { get; }
    }
}
