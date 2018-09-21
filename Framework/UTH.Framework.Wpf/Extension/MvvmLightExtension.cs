using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Navigation;
using Newtonsoft.Json.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// MvvmLight 扩展
    /// </summary>
    public static class MvvmLightExtension
    {
        #region 事件(发送/注册)传递

        public static void SendActionAsync(this string eventName, UTHViewModel view)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                SendAction(eventName, view);
            });
        }
        public static void SendAction(this string eventName, UTHViewModel view)
        {
            Messenger.Default.Send<object>(view, eventName);
        }

        public static void SendActionAsync<T>(this string eventName, UTHViewModel view, T param)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                SendAction<T>(eventName, view, param);
            });
        }
        public static void SendAction<T>(this string eventName, UTHViewModel view, T param)
        {
            Messenger.Default.Send<T>(param, eventName);
        }

        public static void RegistAction(this string eventName, IFormBase win, Action<object> action)
        {
            Messenger.Default.Register(win, eventName, action);
        }

        public static void RegistAction<T>(this string eventName, IFormBase win, Action<T> action)
        {
            Messenger.Default.Register<T>(win, eventName, action);
        }

        #endregion
    }
}
