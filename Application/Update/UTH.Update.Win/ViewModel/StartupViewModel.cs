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

namespace UTH.Update.Win.ViewModel
{
    public class StartupViewModel : UTHViewModel
    {
        public StartupViewModel() : base(culture.Lang.winQiDongQi, "")
        {
            AppHelper.MainApp = AppHelper.GetApp(AppHelper.MainAppCode);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public double ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged(() => ProgressValue); }
        }
        private double _progressValue = 0;

        /// <summary>
        /// ��Ȩ��ϢУ��
        /// </summary>
        public void CheckLicense()
        {
            ProgressValue = 0;

            ////ע�����Ϣ
            //var installDt = AppHelper.GetInstallDt();
            //var userTimes = AppHelper.GetUseTimes();

            ////��Ȩ�ļ�
            //var license = AppHelper.GetLicenseModel();

            ////����
            //if (LicenseFileHelper.IsExpire(license.ExpirationTime, installDt, DateTime.Now))
            //{
            //    throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanYiGuoQi);
            //}

            ////����
            //var licenseUserTimes = IntHelper.Get(license.UseTimes, -1);
            //if (licenseUserTimes != 0 && userTimes > licenseUserTimes)
            //{
            //    throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanShiYongCiShuYiChaoChu);
            //}

            ProgressValue = 10;

            ////��ʽ/���� CPU��MacУ��
            //var currentCPU = Win32Helper.GetCpuID();
            //var currentMac = MacHelper.GetMacAddress();
            //if (BoolHelper.Get(license.IsTrial))
            //{
            //    if (!(license.CPU == currentCPU || license.CPU == "XXXXXXXXXXXXXXXX"))
            //    {
            //        throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
            //    }

            //    if (!(license.Mac == currentMac || license.Mac == "XX-XX-XX-XX-XX-XX"))
            //    {
            //        throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
            //    }
            //}
            //else
            //{
            //    if (!(license.CPU == currentCPU && license.Mac == currentMac))
            //    {
            //        throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
            //    }
            //}

            //ProgressValue = 20;

            ProgressValue = 40;
        }

        /// <summary>
        /// ����汾У��
        /// </summary>
        public void CheckVersion()
        {
            ProgressValue = 45;

            //var currentVersion = VersionHelper.Get();
            //var status = AppHelper.CheckVersion(VersionHelper.Get(), AppHelper.Apps);
            //if (status == EnumUpdateType.Forced)
            //{
            //    WpfHelper.AppUpdate(currentVersion, AppHelper.Apps);
            //}

            ProgressValue = 80;
        }

        /// <summary>
        /// ����ʹ����Ϣ
        /// </summary>
        public void UpdateUse()
        {
            //����ʹ�ô���
            //AppHelper.AddUseTimes();

            ProgressValue = 100;
        }
    }
}