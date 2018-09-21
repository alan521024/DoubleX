namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 超时类型
    /// </summary>
    public enum EnumExpiresType
    {
        Default,

        /// <summary>
        /// 指定过期时间（即在xxxx时间点过期）
        /// </summary>
        Expiration,
        /// <summary>
        /// 弹性过期时间（即在xxxx秒后过期）
        /// </summary>
        Sliding,
    }
}
