namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Web应用程序访问器
    /// </summary>
    public class WebAccessor : DefaultAccessor
    {
        public WebAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public override ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;

        public override Dictionary<string, string> Items
        {
            get
            {
                var _items = new Dictionary<string, string>();
                _items.Add("Culture", WebHelper.GetCulture(_httpContextAccessor.HttpContext));
                _items.Add("AppCode", WebHelper.GetAppCode(_httpContextAccessor.HttpContext));
                _items.Add("ClientIp", WebHelper.GetClientIp(_httpContextAccessor.HttpContext));
                return _items;
            }
        }

    }
}