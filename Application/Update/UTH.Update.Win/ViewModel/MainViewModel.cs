using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
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
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Media.Imaging;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
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

namespace UTH.Update.Win.ViewModel
{
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public class MainViewModel : UTHViewModel
    {
        public MainViewModel() : base(culture.Lang.sysGengXinChengXu, "")
        {
            try
            {
                Initialize();
            }
            catch (Exception ex)
            {
                WpfHelper.SystemLog("UTH.UPDATE.Main", ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// ���°汾
        /// </summary>
        public string NewsVersion { get { return $"{ culture.Lang.sysZuiXinBanBeng} : {AppHelper.MainApp.Versions.No}"; } }

        /// <summary>
        /// ��ǰ�汾
        /// </summary>
        public string CuerrentVersion { get { return $"{culture.Lang.sysDangQianBanBeng} : {AppHelper.MainAppVersion}"; } }

        /// <summary>
        /// �汾����
        /// </summary>
        public string VersionDescript { get { return AppHelper.MainApp.Versions.Descript; } }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; RaisePropertyChanged(() => IsUpdate); }
        }
        private bool _isUpdate;

        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatusTip
        {
            get { return _statusTip; }
            set { _statusTip = value; RaisePropertyChanged(() => StatusTip); }
        }
        private string _statusTip;

        /// <summary>
        /// ���ؽ���
        /// </summary>
        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged(() => ProgressValue); }
        }
        private int _progressValue = 0;

        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public Visibility ProgressVisable
        {
            get { return _progressVisable; }
            set { _progressVisable = value; RaisePropertyChanged(() => ProgressVisable); }
        }
        private Visibility _progressVisable = Visibility.Collapsed;

        /// <summary>
        /// �����¼�
        /// </summary>
        public ICommand OnUpdateCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    AppUpdate();
                });
            }
        }

        /// <summary>
        /// ��ʼ��Ϣ
        /// </summary>
        private void Initialize()
        {
            //TODO:Descript ������ʾhtml ,https://www.cnblogs.com/LemonFive/p/7801471.html
            IsUpdate = AppHelper.MainApp.Versions.No != AppHelper.MainAppVersion;
            if (!IsUpdate)
            {
                StatusTip = culture.Lang.synDangQianWeiZuiXinBanBeng;
            }
        }

        /// <summary>
        /// ���²���
        /// </summary>
        private void AppUpdate()
        {
            if (AppHelper.MainApp.Versions.No == AppHelper.MainAppVersion)
            {
                return;
            }

            ProgressValue = 0;
            StatusTip = "׼�����£����ڹرս���";

            //ɱ����ؽ���
            var killIds = StringHelper.GetToArray(AppHelper.MainAppProcessIds, separator: new string[] { "|" })
                .Select(x => IntHelper.Get(x)).ToArray();
            if (killIds.Length > 0)
            {
                ProcessHelper.Kill(killIds);
            }

            //�����ַ
            var file = FilesHelper.GetFile("../Assets/Update", AppHelper.MainApp.Versions.FileName);
            if (file.Exists)
            {
                file.Delete();
            }

            //�����ļ�
            string downUrl = $"{AppHelper.MainApp.DownloadUrl}?md5={AppHelper.MainApp.Versions.FileMd5}&name={AppHelper.MainApp.Versions.FileName}";
            FilesHelper.DownloadByHttpAsync(downUrl, null, null, file.FullName, (speed, pro) =>
            {
                StatusTip = string.Format(" {0}%  �����ٶ� {1} KB/S", pro, speed);
                ProgressValue = pro;

                if (ProgressVisable != Visibility.Visible)
                {
                    ProgressVisable = Visibility.Visible;
                }
                if (IsUpdate)
                {
                    IsUpdate = false;
                }
                if (pro >= 100)
                {
                    var runFile = ExtractFile(file);
                    AppRun(runFile);
                }
            });
        }

        /// <summary>
        /// ��ѹ/���� �����ļ�
        /// </summary>
        private string ExtractFile(FileInfo file)
        {
            string runFilePath = string.Empty;

            //��ѹ���°������г���
            if (file.Extension.ToLower() == ".zip")
            {
                //��ѹԭ�ļ� �� ��ѹĿ��Ŀ¼
                StatusTip = "������ɣ����ڽ�ѹ�ļ�.....";
                var extDir = new DirectoryInfo($"{ Path.GetTempPath()}/{Guid.NewGuid()}");
                FilesHelper.ExtractFile(file.FullName, extDir.FullName);

                //�滻�ļ�
                StatusTip = "��ѹ�ɹ������ڸ���.....";
                FilesHelper.CopyFold(extDir.FullName, Path.GetDirectoryName(AppHelper.MainAppPath));

                //TODO:����ʱ���Ǹ���һ��ɾ���б�(����ɾ�������ļ�)

                //TODO:��֤�����Ƿ�ɹ�
                //ԭ���Ƕ���ļ�������֤(��ֻ�ж��Ƿ����AppHelper.UpdateAppPath����Ŀ�ִ���ļ�)

                StatusTip = "������ɣ���������.....";
                runFilePath = AppHelper.MainAppPath;
            }

            //ֱ��ִ�и��°�����
            if (file.Extension.ToLower() == ".exe")
            {
                StatusTip = "������ɣ�������������.....";
                runFilePath = file.FullName;
            }

            //Ϊ��ʱĬ������
            if (runFilePath.IsEmpty())
            {
                runFilePath = AppHelper.MainAppPath;
            }

            return runFilePath;
        }

        /// <summary>
        /// �����°�
        /// </summary>
        private void AppRun(string runFile)
        {
            IsUpdate = true;
            ProgressVisable = Visibility.Collapsed;

            if (!runFile.IsEmpty())
            {
                //�����ļ�
                ProcessHelper.Start(runFile, args: "", style: ProcessWindowStyle.Normal);

                //�رո���Ӧ�ó���
                WpfHelper.ExcuteUI(() =>
                {
                    Thread.Sleep(500);
                    ((View.Main)DependencyObj).Close();
                });
            }
            else
            {
                throw new DbxException(EnumCode.��ʾ��Ϣ, "���´���");
            }
        }
    }
}