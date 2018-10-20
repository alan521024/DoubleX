using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Linq;
using System.Globalization;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// WPF访问者
    /// </summary>
    public class WpfAccessor : DefaultAccessor
    {
        /// <summary>
        /// 区域文化
        /// </summary>
        public override CultureInfo Culture { get { return Thread.CurrentThread.CurrentCulture; } }

        /// <summary>
        /// 客户端地址
        /// </summary>
        public override string ClientIp { get { return "127.0.0.2"; } }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        public override string AppCode { get { return EngineHelper.Configuration.AppCode; } }

        /// <summary>
        /// 访问Token
        /// </summary>
        public override string Token { get { return StringHelper.Get(Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Token)?.Value); } }
    }
}
