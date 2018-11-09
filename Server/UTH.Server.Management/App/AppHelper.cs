namespace UTH.Server.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Plug;

    /// <summary>
    /// 应用辅助类
    /// </summary>
    public class AppHelper : AppBaseHelper
    {
        #region 站点信息

        /// <summary>
        /// 站点标题
        /// </summary>
        public static string WebTitle { get { return Lang.mgName; } }

        #endregion

        #region Web操作

        public static bool IsMaster
        {
            get
            {
                return StringHelper.IsEqual(WebHelper.GetQueryValue(WebHelper.GetContext(), "_layout"), "master");
            }
        }

        #endregion

        #region 账号操作

        /// <summary>
        /// 账号签入
        /// </summary>
        /// <param name="token"></param>
        public async static void SignIn(string token)
        {
            if (token.IsEmpty())
                return;

            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var jwtToken = EngineHelper.Resolve<ITokenService>().Resolve(token);

            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim(ClaimTypesExtend.Token, token));
            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);

            await WebHelper.GetContext().SignInAsync(scheme, principal);
        }

        #endregion
    }
}
