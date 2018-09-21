using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 更新类型
    /// </summary>
    public enum EnumUpdateType
    {
        /// <summary>
        /// 无更新
        /// </summary>
        Default = 0, 
        /// <summary>
        /// 增量更新
        /// </summary>
        Incremental = 1,   
        /// <summary>
        /// 强制更新
        /// </summary>
        Forced = 2,        
    }
}
