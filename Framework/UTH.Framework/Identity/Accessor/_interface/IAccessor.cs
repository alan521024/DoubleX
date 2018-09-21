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
        /// 访问数据项
        /// </summary>
        Dictionary<string, string> Items { get; }
    }
}

