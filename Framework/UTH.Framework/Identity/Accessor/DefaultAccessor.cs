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
    /// 默认访问器
    /// </summary>
    public class DefaultAccessor : IAccessor, ISingletonDependency
    {
        /// <summary>
        /// 访问持有者(当事人信息/可有多个Identity)
        /// </summary>
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;

        /// <summary>
        /// 区域文化
        /// </summary>
        public virtual CultureInfo Culture { get { return Thread.CurrentThread.CurrentCulture; } }

        /// <summary>
        /// 客户端地址
        /// </summary>
        public virtual string ClientIp { get { return "127.0.0.1"; } }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        public virtual string AppCode { get { return EngineHelper.Configuration.AppCode; } }

        /// <summary>
        /// 访问Token
        /// </summary>
        public virtual string Token { get; }

    }
}