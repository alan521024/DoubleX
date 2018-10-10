namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using System.Globalization;

    /// <summary>
    /// 访问器接口
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// 身份主体信息
        /// </summary>
        ClaimsPrincipal Principal { get; }

        /// <summary>
        /// 区域文化
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        string ClientIp { get; }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        string AppCode { get;}

        /// <summary>
        /// 访问Token
        /// </summary>
        string Token { get; }
    }
}

