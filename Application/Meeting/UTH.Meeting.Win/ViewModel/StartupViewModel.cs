namespace UTH.Meeting.Win.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using Newtonsoft.Json.Linq;
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
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    public class StartupViewModel : UTHViewModel
    {
        public StartupViewModel() : base(culture.Lang.winQiDongQi, "")
        {
            Initialize();
        }

        #region 私有变量

        #endregion

        #region 公共属性

        /// <summary>
        /// 启动进度
        /// </summary>
        public double ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged(() => ProgressValue); }
        }
        private double _progressValue = 0;

        #endregion

        #region 辅助操作

        private void Initialize()
        {

        }

        #endregion

        /// <summary>
        /// 授权信息校验
        /// </summary>
        public void CheckLicense()
        {
            ProgressValue = 0;

            //注册表信息
            var installDt = AppHelper.GetInstallDt();
            var userTimes = AppHelper.GetUseTimes();

            //授权文件
            var license = AppHelper.GetLicenseModel();

            //过期
            if (LicenseFileHelper.IsExpire(license.ExpirationTime, installDt, DateTime.Now))
            {
                throw new DbxException(EnumCode.授权文件, culture.Lang.sysShouQuanYiGuoQi);
            }

            //次数
            var licenseUserTimes = IntHelper.Get(license.UseTimes, -1);
            if (licenseUserTimes != 0 && userTimes > licenseUserTimes)
            {
                throw new DbxException(EnumCode.授权文件, culture.Lang.sysShouQuanShiYongCiShuYiChaoChu);
            }

            ProgressValue = 10;

            //正式/试用 CPU、Mac校验
            var currentCPU = Win32Helper.GetCpuID();
            var currentMac = MacHelper.GetMacAddress();
            if (BoolHelper.Get(license.IsTrial))
            {
                if (!(license.CPU == currentCPU || license.CPU == "XXXXXXXXXXXXXXXX"))
                {
                    throw new DbxException(EnumCode.授权文件, culture.Lang.sysShouQuanXinXiCuoWu);
                }

                if (!(license.Mac == currentMac || license.Mac == "XX-XX-XX-XX-XX-XX"))
                {
                    throw new DbxException(EnumCode.授权文件, culture.Lang.sysShouQuanXinXiCuoWu);
                }
            }
            else
            {
                if (!(license.CPU == currentCPU && license.Mac == currentMac))
                {
                    throw new DbxException(EnumCode.授权文件, culture.Lang.sysShouQuanXinXiCuoWu);
                }
            }

            ProgressValue = 20;

            //其它验证


            ProgressValue = 40;
        }

        /// <summary>
        /// 程序版本校验
        /// </summary>
        public void CheckVersion()
        {
            ProgressValue = 45;

            var currentVersion = VersionHelper.Get();
            var status = AppHelper.CheckVersion(VersionHelper.Get(), AppHelper.Current);
            if (status == EnumUpdateType.Forced)
            {
                WpfHelper.AppUpdate(currentVersion, AppHelper.Current);
                Environment.Exit(0);
            }

            ProgressValue = 80;
        }

        /// <summary>
        /// 更新使用信息
        /// </summary>
        public void UpdateUse()
        {
            //增加使用次数
            AppHelper.AddUseTimes();

            ProgressValue = 100;
        }
    }
}