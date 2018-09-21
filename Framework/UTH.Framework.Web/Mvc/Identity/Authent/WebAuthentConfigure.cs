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
    /// Web认证配置扩展
    /// </summary>
    internal class WebAuthentConfigure : ConfigureNamedOptions<WebAuthentOptions>
    {
        public WebAuthentConfigure(IConfiguration configuration) :
            base(WebAuthentOptions.DefaultScheme, options => configuration.GetSection(WebAuthentOptions.DefaultScheme).Bind(options))
        {
        }
    }
}
