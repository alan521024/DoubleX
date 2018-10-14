using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Navigation;
using System.Security.Claims;
using System.Security.Principal;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

using Newtonsoft.Json.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using CommonServiceLocator;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// WPF应用程序辅助类
    /// </summary>
    public static class WpfHelper
    {
        // 注销对象方法API
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        #region 系统/注册表/...操作

        //读注册表：
        //    string portName = RegistryGet(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName");

        //写注册表：
        //    SetRegistryData(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName", portName);

        /// <summary>
        /// 读取指定名称的注册表的值
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RegistryGet(RegistryKey root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey != null)
            {
                registData = myKey.GetValue(name).ToString();
            }

            return registData;
        }

        /// <summary>
        /// 向注册表中写数据
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void RegistrySet(RegistryKey root, string subkey, string name, string value)
        {
            RegistryKey aimdir = root.CreateSubKey(subkey);
            aimdir.SetValue(name, value);
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        public static void RegistryDelete(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string aimKey in subkeyNames)
            {
                if (aimKey == name)
                    myKey.DeleteSubKeyTree(name);
            }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool RegistryIsExist(RegistryKey root, string subkey, string name)
        {
            bool _exit = false;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey.IsNull())
            {
                return _exit;
            }

            string[] names = myKey.GetValueNames();
            foreach (string keyName in names)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }

            return _exit;
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static void SystemLog(string source, Exception ex)
        {
            SystemLog(source, ex.IsNull() ? " ex empty " : ex.ToString());
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void SystemLog(string source, string message)
        {
            var log = new EventLog();
            log.Source = source;
            log.WriteEntry(message);
        }

        #endregion

        #region 应用/窗体/控件操作

        /// <summary>
        /// 获取父对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static T GetParent<T>(this DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T) && parent != null)
            {
                var currentWin = parent as Window;
                parent = VisualTreeHelper.GetParent(parent);
                if (parent == null && currentWin != null && currentWin.Owner != null)
                {
                    parent = currentWin.Owner;
                }
            }
            if (parent != null)
                return (T)parent;
            else
                return null;
        }

        /// <summary>
        /// 获取父窗体
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static Window GetParent(this DependencyObject reference)
        {
            return GetParent<Window>(reference);
        }


        //TODO:MessageBox 位置(基本主窗体，非屏幕)
        //http://www.360doc.com/content/09/1117/16/466494_9221971.shtml

        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="owner"></param>
        public static void Message(string msg, string title = null, Action action = null, Window owner = null)
        {
            MessageBoxResult status = MessageBoxResult.No;
            if (title.IsEmpty())
            {
                title = Lang.sysTiShi;
            }

            if (owner.IsNull())
            {
                status = MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                status = MessageBox.Show(owner, msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (status == MessageBoxResult.OK)
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 弹出成功信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="owner"></param>
        public static void Success(string msg, string title = null, Action action = null, Window owner = null)
        {
            MessageBoxResult status = MessageBoxResult.No;
            if (title.IsEmpty())
            {
                title = Lang.sysTiShi;
            }

            if (owner.IsNull())
            {
                status = MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                status = MessageBox.Show(owner, msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (status == MessageBoxResult.OK)
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 弹出错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="owner"></param>
        public static void Error(string msg, string title = null, Action action = null, Window owner = null)
        {
            MessageBoxResult status = MessageBoxResult.No;
            if (title.IsEmpty())
            {
                title = Lang.sysTiShi;
            }

            if (owner.IsNull())
            {
                status = MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                status = MessageBox.Show(owner, msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (status == MessageBoxResult.OK)
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 弹出确认框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="okAction"></param>
        /// <param name="cancelAction"></param>
        /// <param name="owner"></param>
        public static void Confirm(string msg, string title = null, Action okAction = null, Action cancelAction = null, Window owner = null)
        {
            MessageBoxResult status = MessageBoxResult.No;
            if (title.IsEmpty())
            {
                title = Lang.sysTiShi;
            }

            if (owner.IsNull())
            {
                status = MessageBox.Show(msg, title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            }
            else
            {
                status = MessageBox.Show(owner, msg, title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            }

            if (status == MessageBoxResult.OK)
            {
                okAction?.Invoke();
            }
            if (status == MessageBoxResult.Cancel)
            {
                cancelAction?.Invoke();
            }
        }


        /// <summary>
        /// 通知主线程去完成更新(执行方法)
        /// </summary>
        /// <param name="win"></param>
        /// <param name="action"></param>
        public static void ExcuteAction(Window win, Action action)
        {
            //正确的写法：通知主线程去完成更新
            new Thread(() =>
            {
                win.Dispatcher.Invoke(new Action(() =>
                {
                    action();
                }));
            }).Start();
        }

        /// <summary>
        /// 通知主线程去完成更新(执行方法)
        /// </summary>
        /// <param name="action"></param>
        public static void ExcuteUI(Action action)
        {
            ThreadPool.QueueUserWorkItem((Object state) =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(action);
            });
        }

        /// <summary>
        /// 通知主线程去完成更新(执行方法)
        /// </summary>
        /// <param name="call"></param>
        /// <param name="data"></param>
        public static void ExcueUISynchronization(SendOrPostCallback call, object data = null)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Application.Current.Dispatcher));
                SynchronizationContext.Current.Post(call, data);
            });
        }

        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="action"></param>
        public static void AppError(object sender, DispatcherUnhandledExceptionEventArgs e, Action action = null)
        {
            if (EngineHelper.Configuration.IsDebugger)
            {
                e.Handled = true;
            }

            //log
            EngineHelper.LoggingError(e.Exception);

            //DbxException
            DbxException exception = e.Exception as DbxException;
            if (exception.IsNull() && e.Exception.InnerException != null)
            {
                exception = e.Exception.InnerException as DbxException;
            }
            if (exception != null && (exception.Code == EnumCode.提示消息 || exception.Code == EnumCode.校验失败))
            {
                e.Handled = true;
                Error(exception.Message);
                return;
            }
            if (exception != null && exception.Code == EnumCode.初始失败)
            {
                e.Handled = true;
                Error("初始化错误，请检查设备/服务/会议信息并重启应用程序 ", action: action);
                return;
            }

            //MsgException
            string msgText = ExceptionHelper.GetMessage(e.Exception);
            if (e.Exception.InnerException != null)
            {
                msgText = ExceptionHelper.GetMessage(e.Exception.InnerException);
            }
            if (msgText.IsEmpty())
            {
                msgText = "未知错误";
            }
            WpfHelper.Error(string.Format("System Error: {0} {1}", Environment.NewLine, msgText), action: action);
        }

        /// <summary>
        /// 启动更新程序
        /// </summary>
        /// <param name="version"></param>
        /// <param name="model"></param>
        /// <param name="updateExePath"></param>
        public static void AppUpdate(Version version, ApplicationModel model, string updateExePath = null)
        {
            version.CheckNull();
            model.CheckNull();
            if (updateExePath.IsEmpty())
            {
                updateExePath = string.Format(@"{0}/Tool/UTH.Update.Win.exe", AppDomain.CurrentDomain.BaseDirectory);
            }

            string updateArgs = string.Format("{0} {1} {2} {3}", model.Code, version.ToString(), Process.GetCurrentProcess().Id, CodingHelper.UrlEncoding(System.Windows.Forms.Application.ExecutablePath));
            var pros = ProcessHelper.Start(updateExePath, args: updateArgs, style: ProcessWindowStyle.Normal);
            Thread.Sleep(1000);

            //pros.WaitForExit();
            //Application.Current.Shutdown(1);
        }

        #endregion

        #region Remoting

        #endregion

        #region ViewModel

        /// <summary>
        /// 获取单例ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetViewModel<T>() where T : UTHViewModel
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        #endregion

        #region 身份认证

        /// <summary>
        /// 账号签入
        /// </summary>
        /// <param name="token"></param>
        public static void SignIn(string token, IPrincipal principal = null)
        {
            if (token.IsEmpty())
                return;

            var session1 = EngineHelper.Resolve<IApplicationSession>();


            var jwtToken = EngineHelper.Resolve<ITokenService>().Resolve(token);

            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim(GenericIdentity.DefaultNameClaimType, jwtToken.Subject));
            claims.Add(new Claim(GenericIdentity.DefaultRoleClaimType, ""));
            claims.Add(new Claim(WpfClaimTypesExtend.LocalToken, token));

            Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "_win_use"));


            var session2 = EngineHelper.Resolve<IApplicationSession>();
        }

        /// <summary>
        /// 检查是否是管理员身份
        /// </summary>
        public static void CheckAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            bool runAsAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);

            if (!runAsAdmin)
            {
                // It is not possible to launch a ClickOnce app as administrator directly,
                // so instead we launch the app as administrator in a new process.
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                // Start the new process
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    //ex.WriteLog();
                }

                // Shut down the current process
                Environment.Exit(0);
            }
        }

        #endregion

        #region 图片/文档/音视频

        /// <summary>
        /// 转图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToImage(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.StreamSource = ms;
            bit.EndInit();
            return bit;
        }

        /// <summary>
        /// 转图片源
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource BitmapToSource(Bitmap bitmap)
        {
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

        #endregion
    }
}
