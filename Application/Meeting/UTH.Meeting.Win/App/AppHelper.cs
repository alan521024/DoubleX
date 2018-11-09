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
        #region const

        /// <summary>
        /// 倒计时秒
        /// </summary>
        public const double CountdownSecond = 15;


        /// <summary>
        /// 服务名称
        /// </summary>
        public const string ServerName = "UTH.Meeting.Server";

        /// <summary>
        /// 服务路径
        /// </summary>
        public static string ServerPath = $"{AppDomain.CurrentDomain.BaseDirectory}{ServerName}.exe";

        /// <summary>
        /// 服务对象
        /// </summary>
        public static ServerMarshalByRefObject ServerObj
        {
            get
            {

                if (_serverObj == null)
                {
                    IpcClientChannel channel = new IpcClientChannel();
                    ChannelServices.RegisterChannel(channel, false);
                    _serverObj = (ServerMarshalByRefObject)(Activator.GetObject(typeof(ServerMarshalByRefObject), "ipc://channel/ServerMarshalByRefObject.rem"));
                    //RemotingConfiguration.RegisterWellKnownClientType(typeof(RecognizeRemotingService), "ipc://channel/RecognizeRemotingService.rem");
                    //serverRemoteObj = new RecognizeRemotingService();
                }
                return _serverObj;
            }
        }
        static ServerMarshalByRefObject _serverObj = null;

        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                return $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}{AppDomain.CurrentDomain.SetupInformation.ApplicationName}";
            }
        }

        /// <summary>
        /// 更新工具路径
        /// </summary>
        public static string UpdateToolPath
        {
            get
            {
                return $"{AppDomain.CurrentDomain.BaseDirectory}/Tool/UTH.Update.Win.exe";
            }
        }


        #endregion

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
        /// 获取应用程序相关进程Id
        /// </summary>
        /// <returns></returns>
        public static List<int> GetAppProcessIds()
        {
            var ids = new List<int>();
            ids.Add(Process.GetCurrentProcess().Id);

            var serverNames = Process.GetProcessesByName("UTH.Meeting.Server");
            if (!serverNames.IsEmpty())
            {
                ids.AddRange(serverNames.Select(x => x.Id).ToList());
            }

            return ids;
        }

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

        /// <summary>
        /// 授权文件信息
        /// </summary>
        public static LicenseModel Licenses { get { return GetLicenseModel(); } }

        /// <summary>
        /// 获取面包屑导航
        /// </summary>
        /// <param name="navs"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Main面包屑导航
        /// </summary>
        /// <param name="main"></param>
        /// <param name="data"></param>
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

        /// <summary>
        /// 应用程序更新
        /// </summary>
        public static void AppUpdate(bool isCloseAll = false, bool isOnlyForced = false)
        {
            var cur = VersionHelper.Get();
            var last = new Version(CurrentApp.Versions.No);
            var updateType = WpfHelper.GetAppUpdateType(cur, last);
            if (CurrentApp.Versions.UpdateType == EnumUpdateType.Forced)
            {
                updateType = EnumUpdateType.Forced;
            }

            if (isOnlyForced && updateType != EnumUpdateType.Forced)
            {
                //仅强制更新的时候操作,非强制更新，跳出
                return;
            }

            var currentIds = GetAppProcessIds();
            var upProcess = WpfHelper.AppUpdate(cur, CurrentApp.Application.Code, ApplicationPath, UpdateToolPath, currentIds.ToArray());
            if (isCloseAll)
            {
                Thread.Sleep(800);
                ProcessHelper.Kill(currentIds.Where(x => x != Process.GetCurrentProcess().Id).ToArray());
                Thread.Sleep(200);
                ProcessHelper.Kill(Process.GetCurrentProcess().Id);
            }
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
