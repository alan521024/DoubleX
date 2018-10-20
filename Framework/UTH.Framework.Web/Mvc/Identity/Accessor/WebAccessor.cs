namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Globalization;
    using Microsoft.AspNetCore.Http;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Web应用程序访问器
    /// </summary>
    public class WebAccessor : DefaultAccessor, IAccessor
    {
        public WebAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// 访问持有者(当事人信息/可有多个Identity)
        /// </summary>
        public override ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;

        /// <summary>
        /// 区域文化
        /// </summary>
        public override CultureInfo Culture { get { return new CultureInfo(WebHelper.GetCulture(_httpContextAccessor.HttpContext)); } }

        /// <summary>
        /// 客户端地址
        /// </summary>
        public override string ClientIp { get { return WebHelper.GetClientIp(_httpContextAccessor.HttpContext); } }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        public override string AppCode { get { return WebHelper.GetAppCode(_httpContextAccessor.HttpContext); } }

        /// <summary>
        /// 访问Token
        /// </summary>
        public override string Token { get { return StringHelper.Get(Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Token)?.Value); } }
    }
}