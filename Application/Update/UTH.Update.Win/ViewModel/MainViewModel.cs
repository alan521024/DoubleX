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

        #region ��������

        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatuText
        {
            get { return _statuText; }
            set { _statuText = value; RaisePropertyChanged(() => StatuText); }
        }
        private string _statuText;

        /// <summary>
        /// �汾����
        /// </summary>
        public string VersionDescript { get; set; }

        /// <summary>
        /// ���°汾
        /// </summary>
        public string NewsVersionText { get; set; }

        /// <summary>
        /// ��ǰ�汾
        /// </summary>
        public string CuerrentVersionText { get; set; }

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
        /// ���ؽ���
        /// </summary>
        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged(() => ProgressValue); }
        }
        private int _progressValue = 0;

        /// <summary>
        /// �Ƿ���ʾ���ؽ�����
        /// </summary>
        public Visibility ProgressVisable
        {
            get { return _progressVisable; }
            set { _progressVisable = value; RaisePropertyChanged(() => ProgressVisable); }
        }
        private Visibility _progressVisable = Visibility.Collapsed;

        #endregion

        #region ˽�б���

        #endregion

        #region ��������

        private void Initialize()
        {
            //TODO:Descript ������ʾhtml
            //https://www.cnblogs.com/LemonFive/p/7801471.html

            IsUpdate = AppHelper.Current.Versions.No.ToString() != AppHelper.ExecuteAppVersion;
            if (!IsUpdate)
            {
                StatuText = culture.Lang.synDangQianWeiZuiXinBanBeng;
            }

            NewsVersionText = string.Format("{0} : {1}", culture.Lang.sysZuiXinBanBeng, AppHelper.Current.Versions.No.ToString());
            CuerrentVersionText = string.Format("{0} : {1}", culture.Lang.sysDangQianBanBeng, AppHelper.ExecuteAppVersion);
            VersionDescript = AppHelper.Current.Versions.Descript;
        }

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private string GetAppUpdateFileSavePath(ApplicationVersion version)
        {
            version.CheckNull();
            return string.Format(@"{0}\\Assets\\Update\\{1}",
                AppDomain.CurrentDomain.BaseDirectory, version.FileName);
        }

        #endregion

        /// <summary>
        /// Ӧ�ø���
        /// </summary>
        public void UpdateApp(ApplicationVersion version, Action<ApplicationVersion, string> action)
        {
            ProgressValue = 0;

            if (version.No.ToString() == AppHelper.ExecuteAppVersion)
            {
                WpfHelper.ExcuteUI(() =>
                {
                    StatuText = culture.Lang.synDangQianWeiZuiXinBanBeng;
                });
                return;
            }

            string savePath = string.Format(@"{0}Assets\\Update\\{1}", AppDomain.CurrentDomain.BaseDirectory, version.FileName);

            FilesHelper.DeleteFile(savePath);
            FilesHelper.CreateFold(savePath, isFilePath: true);

            string downUrl = string.Format("{0}?md5={1}&name={2}", version.FileAddress, version.FileMd5, version.FileName);
            FilesHelper.ClientHttpAsyncDownloadFile(downUrl, null, null, savePath, (speed, pro) =>
            {
                StatuText = string.Format(" {0}%  �����ٶ� {1} KB/S", pro, speed);
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
                    action?.Invoke(version, savePath);
                }
            });
        }

        /// <summary>
        /// �ļ�����
        /// </summary>
        public string UpdateFile(ApplicationVersion version, string filePath)
        {
            ProgressVisable = Visibility.Collapsed;

            filePath.CheckEmpty();
            var fileExtension = Path.GetExtension(filePath).ToLower();

            string runFilePath = string.Empty;

            //��ѹ���°������г���
            if (fileExtension == ".zip")
            {
                //��ѹԭ�ļ� �� ��ѹĿ��Ŀ¼
                StatuText = "������ɣ����ڽ�ѹ�ļ�.....";
                var extFold = string.Format("{0}\\{1}", Path.GetTempPath(), Guid.NewGuid());
                FilesHelper.ExtractFile(filePath, extFold);

                //�滻�ļ�
                StatuText = "��ѹ�ɹ������ڸ���.....";
                FilesHelper.CopyFold(extFold, Path.GetDirectoryName(AppHelper.ExecuteAppPath));

                //TODO:����ʱ���Ǹ���һ��ɾ���б�(����ɾ�������ļ�)

                //TODO:��֤�����Ƿ�ɹ�
                //ԭ���Ƕ���ļ�������֤(��ֻ�ж��Ƿ����AppHelper.UpdateAppPath����Ŀ�ִ���ļ�)

                StatuText = "������ɣ���������.....";

                //���������ļ�
                runFilePath = AppHelper.ExecuteAppPath;
            }

            //ֱ��ִ�и��°�����
            if (fileExtension == ".exe")
            {
                StatuText = "������ɣ�������������.....";

                //���������ļ�
                runFilePath = filePath;
            }
            return runFilePath;
        }

    }
}