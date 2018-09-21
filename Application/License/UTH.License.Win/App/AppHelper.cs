using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using UTH.Infrastructure.Resource;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.License.Win
{
    /// <summary>
    /// 应用辅助类
    /// </summary> 
    public class AppHelper : AppBaseHelper
    {
        #region 使用次数 示例，本工具未使用。

        /// <summary>
        /// 注山表(UTH路径)
        /// </summary>
        public const string REGISTRYPATH = "SOFTWARE\\UTH\\License";

        /// <summary>
        /// 注册表(安装时间)
        /// </summary>
        public const string INSTALLDT = "InstallDt";

        /// <summary>
        /// 注册表(使用次数)
        /// </summary>
        public const string USETIMES = "UseTimes";

        /// <summary>
        /// 获取安装时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetInstallDt()
        {
            if (!WpfHelper.RegistryIsExist(Registry.LocalMachine, REGISTRYPATH, INSTALLDT))
            {
                WpfHelper.RegistrySet(Registry.LocalMachine, REGISTRYPATH, INSTALLDT, StringHelper.Get(DateTime.Now));
            }
            var installDt = DateTimeHelper.Get(WpfHelper.RegistryGet(Registry.LocalMachine, REGISTRYPATH, INSTALLDT));
            if (installDt <= DateTimeHelper.DefaultDateTime)
            {
                throw new DbxException(EnumCode.注册表异常);
            }
            return installDt;

        }

        /// <summary>
        /// 获取使用次数
        /// </summary>
        /// <returns></returns>
        public static int GetUseTimes()
        {
            if (!WpfHelper.RegistryIsExist(Registry.LocalMachine, REGISTRYPATH, USETIMES))
            {
                WpfHelper.RegistrySet(Registry.LocalMachine, REGISTRYPATH, USETIMES, "0");
            }
            var userTimes = IntHelper.Get(WpfHelper.RegistryGet(Registry.LocalMachine, REGISTRYPATH, USETIMES), -1);
            if (userTimes < 0)
            {
                throw new DbxException(EnumCode.注册表异常);
            }
            return userTimes;
        }

        /// <summary>
        /// 增加使用次数
        /// </summary>
        /// <returns></returns>
        public static void AddUseTimes()
        {
            if (!WpfHelper.RegistryIsExist(Registry.LocalMachine, REGISTRYPATH, USETIMES))
            {
                throw new DbxException(EnumCode.注册表异常);
            }

            var userTimes = IntHelper.Get(WpfHelper.RegistryGet(Registry.LocalMachine, REGISTRYPATH, USETIMES), -1);
            if (userTimes < 0)
            {
                throw new DbxException(EnumCode.注册表异常);
            }

            WpfHelper.RegistrySet(Registry.LocalMachine, REGISTRYPATH, USETIMES, (userTimes + 1).ToString());
        }

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
