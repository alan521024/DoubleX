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
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// 事件列表
    /// </summary>
    public class CommonEventModel
    {
        /// <summary>
        /// 开始一个程序进程
        /// </summary>
        public const string ProcessStart = "PROCESSSTART";

        /// <summary>
        /// 内容消息
        /// </summary>
        public const string MessageTip = "MESSAGETIP";
        /// <summary>
        /// 内容消息+回调
        /// </summary>
        public const string MessageCall = "MESSAGECALL";

        /// <summary>
        /// 成功消息
        /// </summary>
        public const string SuccessTip = "SUCCESSTIP";
        /// <summary>
        /// 成功消息+回调
        /// </summary>
        public const string SuccessCall = "SUCCESSCALL";

        /// <summary>
        /// 错误消息
        /// </summary>
        public const string ErrorTip = "ERRORTIP";
        /// <summary>
        /// 错误消息+回调
        /// </summary>
        public const string ErrorCall = "ERRORCALL";

        /// <summary>
        /// 确认消息
        /// </summary>
        public const string ConfirmTip = "CONFIRMTIP";
        /// <summary>
        /// 确认消息+回调
        /// </summary>
        public const string ConfirmCall = "CONFIRMCALL";


        /// <summary>
        /// 启动页完成后跳转登录
        /// </summary>
        public const string StartupClose = "STARTUPCLOSE";

        /// <summary>
        /// 打开主界面
        /// </summary>
        public const string OpenMain = "OPENMAIN";


        /// <summary>
        /// 公共事件注册
        /// </summary>
        public static void Regist(IFormBase recipient)
        {
            ProcessStart.RegistAction<TParam<string, Action>>(recipient, (m) =>
             {
                 if (!m.Param1.IsEmpty())
                 {
                     System.Diagnostics.Process.Start(m.Param1);
                 }
                 m.Param2?.Invoke();
             });


            MessageTip.RegistAction<string>(recipient, (m) => WpfHelper.Message(m));

            MessageCall.RegistAction<TParam<string, Action, bool>>(recipient, (m) =>
            {
                if (m.Param3)
                {
                    WpfHelper.Error(m.Param1, m.Param2);
                }
                else
                {
                    WpfHelper.Message(m.Param1, m.Param2);
                }
            });


            SuccessTip.RegistAction<string>(recipient, (m) => WpfHelper.Success(m));

            SuccessCall.RegistAction<TParam<string, Action>>(recipient, (m) => WpfHelper.Success(m.Param1, m.Param2));


            ErrorTip.RegistAction<string>(recipient, (m) => WpfHelper.Error(m));

            ErrorCall.RegistAction<TParam<string, Action>>(recipient, (m) => WpfHelper.Error(m.Param1, m.Param2));


            ConfirmTip.RegistAction<string>(recipient, (m) => WpfHelper.Confirm(m));

            ConfirmCall.RegistAction<TParam<string, Action, Action>>(recipient, (m) => WpfHelper.Confirm(m.Param1, m.Param2, m.Param3));
        }

    }
}
