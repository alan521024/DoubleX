using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// 认证配置类型
    /// </summary>
    public enum EnumAuthenticationType
    {
        None = 0,
        /// <summary>
        /// CookieCollection
        /// </summary>
        Cookie = 1,
        /// <summary>
        /// Jwt
        /// </summary>
        Jwt=2,
        /// <summary>
        /// UTH 自定义认证(使用Redis 存储 Session)
        /// </summary>
        UTH = 3
    }
}
