namespace UTH.Framework.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Principal;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// WpfClaimsTypes 扩展(ClaimTypes / 身份信息元素类型)
    /// see all : System.Security.Claims.ClaimTypes 
    /// </summary>
    public static class WpfClaimTypesExtend
    {
        /// <summary>
        /// Local Token
        /// </summary>
        public const string LocalToken = "localToken";

    }
}
