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
    using System.Diagnostics;

    public class StartupViewModel : UTHViewModel
    {
        public StartupViewModel() : base(culture.Lang.winQiDongQi, "")
        {

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

        public void Start()
        {
            new Thread(() =>
            {
                WpfHelper.ExcuteUI(() =>
                {
                    CheckLicense();
                    UpdateVersion();
                    UpdateAppUse();
                    CopyRes();
                    WpfHelper.ExcuteUI(ToLogin);
                });
            }).Start();
        }

        /// <summary>
        /// ��Ȩ��ϢУ��
        /// </summary>
        public void CheckLicense()
        {
            ProgressValue = 0;

            //ע�����Ϣ
            var installDt = AppHelper.GetInstallDt();
            var userTimes = AppHelper.GetUseTimes();

            //��Ȩ�ļ�
            var license = AppHelper.GetLicenseModel();

            //����
            if (LicenseFileHelper.IsExpire(license.ExpirationTime, installDt, DateTime.Now))
            {
                throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanYiGuoQi);
            }

            //����
            var licenseUserTimes = IntHelper.Get(license.UseTimes, -1);
            if (licenseUserTimes != 0 && userTimes > licenseUserTimes)
            {
                throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanShiYongCiShuYiChaoChu);
            }

            ProgressValue = 10;

            //��ʽ/���� CPU��MacУ��
            var currentCPU = Win32Helper.GetCpuID();
            var currentMac = MacHelper.GetMacAddress();
            if (BoolHelper.Get(license.IsTrial))
            {
                if (!(license.CPU == currentCPU || license.CPU == "XXXXXXXXXXXXXXXX"))
                {
                    throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
                }

                if (!(license.Mac == currentMac || license.Mac == "XX-XX-XX-XX-XX-XX"))
                {
                    throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
                }
            }
            else
            {
                if (!(license.CPU == currentCPU && license.Mac == currentMac))
                {
                    throw new DbxException(EnumCode.��Ȩ�ļ�, culture.Lang.sysShouQuanXinXiCuoWu);
                }
            }

            ProgressValue = 20;

            //������֤


            ProgressValue = 40;
        }

        /// <summary>
        /// ����Ӧ�ð汾�汾
        /// </summary>
        public void UpdateVersion()
        {
            ProgressValue = 45;
            AppHelper.AppUpdate(isCloseAll: true, isOnlyForced: true);
            ProgressValue = 80;
        }

        /// <summary>
        /// ����Ӧ��ʹ�ü�¼
        /// </summary>
        public void UpdateAppUse()
        {
            //����ʹ�ô���
            AppHelper.AddUseTimes();

            ProgressValue = 100;
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        private void CopyRes()
        {
            try
            {
                //ɱ����ؽ��̽���
                ProcessHelper.Kill("UTH.Update.Win");
                var sourceDir = FilesHelper.GetDirectory("Assets/Res");
                var destDir = FilesHelper.GetDirectory(".");
                FilesHelper.CopyFold(sourceDir.FullName, destDir.FullName);
                foreach (var dir in sourceDir.GetDirectories()) {
                    System.IO.Directory.Delete(dir.FullName,true);
                }
                foreach (var file in sourceDir.GetFiles())
                {
                    System.IO.File.Delete(file.FullName);
                }
            }
            catch(Exception ex) {
                EngineHelper.LoggingError(ex);
                throw new DbxException(EnumCode.��ʾ��Ϣ, culture.Lang.sysChuShiJieXiZiYuanShiBaiQingGuanBiSuoYouXiangGuanXinXi);
            }
        }

        /// <summary>
        /// ��ת��¼
        /// </summary>
        protected void ToLogin()
        {
            View._LayoutAccount form = new View._LayoutAccount();
            form.Show();
            ((UTHWindow)DependencyObj).Close();
        }
    }
}