using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Update.Win
{
    /// <summary>
    /// 应用辅助类
    /// </summary> 
    public class AppHelper : AppBaseHelper
    {
        #region 全局信息

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public static ApplicationModel Current
        {
            get
            {
                ExecuteAppCode.CheckEmpty();
                if (_current == null)
                {
                    _current = GetApplication(ExecuteAppCode);
                }
                return _current;
            }
        }
        private static ApplicationModel _current;

        /// <summary>
        /// 执行程序程序Id
        /// </summary>
        public static string ExecuteAppCode { get; set; }

        /// <summary>
        /// 执行程序版本
        /// </summary>
        public static string ExecuteAppVersion { get; set; }

        /// <summary>
        /// 执行程序路径
        /// </summary>
        public static string ExecuteAppPath { get; set; }

        /// <summary>
        /// 执行程序进程(需杀掉的)
        /// </summary>
        public static string ExecuteAppProcessIds { get; set; }

        #endregion

        #region 账号操作

        ///// <summary>
        ///// 账号签入
        ///// </summary>
        ///// <param name="token"></param>
        //public async static void SignIn(string token)
        //{
        //    if (token.IsEmpty())
        //        return;
        //    var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    var jwtToken = EngineHelper.Resolve<ITokenService>().Resolve(token);
        //    var claims = jwtToken.Claims.ToList();
        //    claims.Add(new Claim(DbxClaimTypes.Token, token));
        //    await WebHelper.GetContext().SignInAsync(scheme, new ClaimsPrincipal(new ClaimsIdentity(claims, scheme)));
        //}

        #endregion
    }
}
