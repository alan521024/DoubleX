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

        #region 系统

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

        #region 注册表

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

        #endregion

        #region 窗体/消息/UI...

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
        [Obsolete("该方法己过时，仅供参考，请调用ExcuteUI")]
        public static void ExcueUISynchronization(SendOrPostCallback call, object data = null)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Application.Current.Dispatcher));
                SynchronizationContext.Current.Post(call, data);
            });
        }

        /// <summary>
        /// 通知主线程去完成更新(执行方法)
        /// </summary>
        /// <param name="win"></param>
        /// <param name="action"></param>
        [Obsolete("该方法己过时，仅供参考，请调用ExcuteUI")]
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

        #region 控件(图片/文档/音视频)

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

        #region 应用程序

        /// <summary>
        /// 应用程序(当前运行)仅能运行一个进程
        /// </summary>
        public static void AppIsOnlyRun()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            if (processes.Where(x => x.Id != currentProcess.Id).Count() > 0)
            {
                WpfHelper.Message(Lang.sysChengXuYiJingYunXingQingWuChongFuCaoZuo);
                Environment.Exit(1);
            }
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
            DbxException exp = e.Exception as DbxException;
            if (exp.IsNull() && e.Exception.InnerException != null)
            {
                exp = e.Exception.InnerException as DbxException;
            }

            if (exp != null && (exp.Code == EnumCode.提示消息 || exp.Code == EnumCode.校验失败))
            {
                e.Handled = true;
                Error(exp.Message);
                return;
            }

            if (exp != null && exp.Code == EnumCode.初始失败)
            {
                e.Handled = true;
                Error("初始化错误，请检查设备/服务/会议信息并重启应用程序 ", action: action);
                return;
            }

            //other msg
            string msg = ExceptionHelper.GetMessage(e.Exception);
            if (e.Exception.InnerException != null)
            {
                msg = ExceptionHelper.GetMessage(e.Exception.InnerException);
            }
            if (msg.IsEmpty())
            {
                msg = "未知错误";
            }
            Error(string.Format("System Error: {0} {1}", Environment.NewLine, msg), action: action);
        }

        /// <summary>
        /// 启动更新程序
        /// </summary>
        /// <param name="version">当前版本</param>
        /// <param name="model">最新版本</param>
        /// <param name="appPath">应用程序目录</param>
        /// <param name="toolPath">更新工具目录</param>
        /// <param name="processIds">要删除的进程Id</param>
        public static Process AppUpdate(Version version, ApplicationModel model, string appPath, string toolPath, params int[] processIds)
        {
            version.CheckNull();
            model.CheckNull();
            processIds.CheckEmpty();
            
            string updateArgs = $"{model.Code} {version.ToString()} {StringHelper.Get(processIds, "|")} {CodingHelper.UrlEncoding(appPath)}";
            return ProcessHelper.Start(toolPath, args: updateArgs, style: ProcessWindowStyle.Normal);
        }

        #endregion

        #region 应用服务

        /// <summary>
        /// 应用服务启动
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="serverPath"></param>
        /// <param name="serverObj"></param>
        /// <param name="isOnly"></param>
        /// <param name="startAction"></param>
        public static Process AppServerStart(string serverName, string serverPath, IServerMarshalByRefObject serverObj, bool isOnly = false, Action<int, int> startAction = null)
        {
            if (isOnly)
            {
                AppServerClose(serverName);
            }

            //System.Runtime.Remoting.RemotingException:“向 IPC 端口写入失败: 管道正在被关闭。
            //System.Runtime.Remoting.RemotingException:“连接到 IPC 端口失败: 系统找不到指定的文件。
            var serverProcess = ProcessHelper.Start(serverPath, style: EngineHelper.Configuration.IsDebugger ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Hidden);

            //loop check isconnection and out 30s exception
            bool loaded = false;
            int current = 0, error = 0, max = 300;
            while (!loaded)
            {
                startAction?.Invoke(current, max);

                if (current > max)
                {
                    loaded = true;
                    throw new DbxException(EnumCode.服务异常, "应用程序服务启动失败");
                }

                try
                {
                    loaded = serverObj.IsConnection;
                }
                catch (Exception ex)
                {
                    error++;
                    EngineHelper.LoggingError(ex);
                }

                current++;

                Thread.Sleep(100);
            }

            return serverProcess;
        }

        /// <summary>
        /// 应用服务关闭
        /// </summary>
        /// <param name="serverName"></param>
        public static void AppServerClose(string serverName)
        {
            var error = 0;
            while (Process.GetProcessesByName(serverName).Length > 0)
            {
                //30s
                if (error > 300)
                {
                    throw new DbxException(EnumCode.服务异常, "应用程序服务停止失败");
                    //break;
                }
                ProcessHelper.Kill(serverName);
                Thread.Sleep(100);
                error++;
            }
        }

        #endregion

        #region 身份认证

        /// <summary>
        /// 账号签入
        /// </summary>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        public static void SignIn(string token, IPrincipal principal = null)
        {
            if (token.IsEmpty())
                return;

            var jwtToken = EngineHelper.Resolve<ITokenService>().Resolve(token);

            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim(GenericIdentity.DefaultNameClaimType, jwtToken.Subject));
            claims.Add(new Claim(GenericIdentity.DefaultRoleClaimType, ""));
            claims.Add(new Claim(ClaimTypesExtend.Token, token));

            Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "_win_use"));
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

    }
}
