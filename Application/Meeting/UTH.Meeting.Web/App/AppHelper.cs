namespace UTH.Meeting.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
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

            ////var principal = new ClaimsPrincipal(identity);
            ////HttpContext.Authentication.SignInAsync("member", principal, new AuthenticationProperties { IsPersistent = true });
            ////HttpContext.SignInAsync(scheme, new ClaimsPrincipal(new ClaimsIdentity(claims, scheme)));
            ////HttpContext.SignInAsync(scheme, new ClaimsPrincipal(new ClaimsIdentity(claims, scheme)));

            //var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //var jwtToken = EngineHelper.Resolve<ITokenService>().Resolve(token);
            //var claims = jwtToken.Claims.ToList();
            //claims.Add(new Claim(ClaimTypesExtend.Token, token));
            //await WebHelper.GetContext().SignInAsync(scheme, new ClaimsPrincipal(new ClaimsIdentity(claims, scheme)));
        }

        #endregion

        #region 会议业务资料

        /// <summary>
        /// 源语言列表
        /// </summary>
        public static List<KeyValueModel> MeetingSourceLangs
        {
            get
            {
                if (_meetingSourceLangs.IsEmpty())
                {
                    StringHelper.GetToArray(EngineHelper.Configuration.Settings.GetValue("meetingSource"), new string[] { "," }).ToList().ForEach(x =>
                    {
                        var items = StringHelper.GetToArray(x, new string[] { "|" });
                        if (items.Length == 2)
                        {
                            _meetingSourceLangs.Add(new KeyValueModel(items[0], items[1].ConvertLang()));
                        }
                    });
                }
                return _meetingSourceLangs;
            }
        }
        private static List<KeyValueModel> _meetingSourceLangs = new List<KeyValueModel>();

        /// <summary>
        /// 目标语言列表
        /// </summary>
        public static List<KeyValueModel> MeetingTargetLangs
        {
            get
            {
                if (_mseetingTargetLangs.IsEmpty())
                {
                    StringHelper.GetToArray(EngineHelper.Configuration.Settings.GetValue("meetingTarget"), new string[] { "," }).ToList().ForEach(x =>
                    {
                        var items = StringHelper.GetToArray(x, new string[] { "|" });
                        if (items.Length == 2)
                        {
                            _mseetingTargetLangs.Add(new KeyValueModel(items[0], items[1].ConvertLang()));
                        }
                    });
                }
                return _mseetingTargetLangs;
            }
        }
        private static List<KeyValueModel> _mseetingTargetLangs = new List<KeyValueModel>();

        #endregion
    }
}
