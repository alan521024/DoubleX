namespace UTH.Meeting.Win
{
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
    using System.Diagnostics;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Collections.ObjectModel;
    using Microsoft.Win32;
    using Newtonsoft.Json.Linq;
    using MahApps.Metro.Controls;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using Autofac;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;
    using UTH.Meeting.Server;

    /// <summary>
    /// 应用辅助类
    /// </summary> 
    public class AppHelper : AppBaseHelper
    {
        #region 注册表（使用时间/次数）

        /// <summary>
        /// 注册表(UTH路径)
        /// </summary>
        public const string REGISTRYPATH = "SOFTWARE\\UTH";

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

        #region 应用程序/授权文件

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public static ApplicationModel Current { get { return GetApplication(); } }

        /// <summary>
        /// 授权文件信息
        /// </summary>
        public static LicenseModel Licenses { get { return GetLicenseModel(); } }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string AppTitle
        {
            get
            {
                return string.Format("{0}-{1}", culture.Lang.sysCompany, culture.Lang.sysTitle);
            }
        }

        public static ObservableCollection<CrumbData> GetMainNavigationCrumbs(params CrumbData[] navs)
        {
            var items = new ObservableCollection<CrumbData>();

            items.Add(new CrumbData() { Text = culture.Lang.sysFanHuiShangJi, IsBack = true, Split = "|" });
            items.Add(new CrumbData() { Text = culture.Lang.metHuiYiJieMian, IsHome = true });
            Array.ForEach(navs, x =>
            {
                items.Add(x);
            });
            return items;
        }

        public static void MainNavigationCrumbsAction(Win.View.Main main, CrumbData data)
        {
            main.CheckNull();
            data.CheckNull();

            if (!main.IsNull())
            {
                if (data.IsBack)
                {
                    main.mainFrame.GoBack();
                }
                if (data.IsHome)
                {
                    main.ShowMeeting();
                }
            }
        }

        #endregion

        #region 服务/设备/语音

        /// <summary>
        /// 服务启动
        /// </summary>
        /// <param name="server"></param>
        /// <param name="isClose"></param>
        public static void ServerStart(string server = "UTH.Meeting.Server", bool isClose = false, Action<int, int> action = null)
        {
            if (isClose)
            {
                ProcessHelper.Kill(server);
            }

            //System.Runtime.Remoting.RemotingException:“向 IPC 端口写入失败: 管道正在被关闭。
            //System.Runtime.Remoting.RemotingException:“连接到 IPC 端口失败: 系统找不到指定的文件。
            var progress = ProcessHelper.Start($"{AppDomain.CurrentDomain.BaseDirectory}{server}.exe", style: EngineHelper.Configuration.IsDebugger ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Hidden);

            var serverObj = GetServerMarshalByRefObject();

            // 1000*30 30秒后未启动异常
            //loop check isconnection 
            bool loaded = false;
            int current = 0, total = 30;
            while (!loaded)
            {
                action?.Invoke(current, total);

                try
                {
                    loaded = serverObj.IsConnection;
                }
                catch (Exception ex)
                {
                    if (current > total)
                    {
                        throw ex;
                    }
                    current++;
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 服务关闭
        /// </summary>
        /// <param name="server"></param>
        public static void ServerClose(string server = "UTH.Meeting.Server")
        {
            if (!server.IsEmpty())
            {
                ProcessHelper.Kill(server);
            }
        }

        /// <summary>
        /// 远程服务获取
        /// </summary>
        /// <param name="server"></param>
        public static ServerMarshalByRefObject GetServerMarshalByRefObject()
        {
            //后台远程服务
            if (serverObj == null)
            {
                IpcClientChannel channel = new IpcClientChannel();
                ChannelServices.RegisterChannel(channel, false);
                serverObj = (ServerMarshalByRefObject)(Activator.GetObject(typeof(ServerMarshalByRefObject), "ipc://channel/ServerMarshalByRefObject.rem"));
                //RemotingConfiguration.RegisterWellKnownClientType(typeof(RecognizeRemotingService), "ipc://channel/RecognizeRemotingService.rem");
                //serverRemoteObj = new RecognizeRemotingService();
            }
            return serverObj;
        }
        static ServerMarshalByRefObject serverObj = null;

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
