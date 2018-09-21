namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Authentication;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Web认证配置信息
    /// </summary>
    public class WebAuthentOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "_DefaultAuthentication";
    }
}
