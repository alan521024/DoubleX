namespace UTH.Server.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Plug;

    [Authorize]
    public class AuthBase : WebViewBase
    {
        /// <summary>
        /// 会话信息
        /// </summary>
        public IApplicationSession Current { get; set; }

    }
}
