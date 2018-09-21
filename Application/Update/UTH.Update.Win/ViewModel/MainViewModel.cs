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
    /// 主界面
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

        #region 公共属性

        /// <summary>
        /// 状态内容
        /// </summary>
        public string StatuText
        {
            get { return _statuText; }
            set { _statuText = value; RaisePropertyChanged(() => StatuText); }
        }
        private string _statuText;

        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionDescript { get; set; }

        /// <summary>
        /// 最新版本
        /// </summary>
        public string NewsVersionText { get; set; }

        /// <summary>
        /// 当前版本
        /// </summary>
        public string CuerrentVersionText { get; set; }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; RaisePropertyChanged(() => IsUpdate); }
        }
        private bool _isUpdate;

        /// <summary>
        /// 下载进度
        /// </summary>
        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged(() => ProgressValue); }
        }
        private int _progressValue = 0;

        /// <summary>
        /// 是否显示下载进度条
        /// </summary>
        public Visibility ProgressVisable
        {
            get { return _progressVisable; }
            set { _progressVisable = value; RaisePropertyChanged(() => ProgressVisable); }
        }
        private Visibility _progressVisable = Visibility.Collapsed;

        #endregion

        #region 私有变量

        #endregion

        #region 辅助操作

        private void Initialize()
        {
            //TODO:Descript 考虑显示html
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
        /// 更新文件保存路径
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
        /// 应用更新
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
                StatuText = string.Format(" {0}%  下载速度 {1} KB/S", pro, speed);
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
        /// 文件处理
        /// </summary>
        public string UpdateFile(ApplicationVersion version, string filePath)
        {
            ProgressVisable = Visibility.Collapsed;

            filePath.CheckEmpty();
            var fileExtension = Path.GetExtension(filePath).ToLower();

            string runFilePath = string.Empty;

            //解压更新包后运行程序
            if (fileExtension == ".zip")
            {
                //解压原文件 及 解压目标目录
                StatuText = "下载完成，正在解压文件.....";
                var extFold = string.Format("{0}\\{1}", Path.GetTempPath(), Guid.NewGuid());
                FilesHelper.ExtractFile(filePath, extFold);

                //替换文件
                StatuText = "解压成功，正在更新.....";
                FilesHelper.CopyFold(extFold, Path.GetDirectoryName(AppHelper.ExecuteAppPath));

                //TODO:更新时考虑附带一个删除列表(用于删除无用文件)

                //TODO:验证更新是否成功
                //原则是多个文件都需验证(暂只判断是否存在AppHelper.UpdateAppPath传入的可执行文件)

                StatuText = "更新完成，正在启动.....";

                //更新启动文件
                runFilePath = AppHelper.ExecuteAppPath;
            }

            //直接执行更新包程序
            if (fileExtension == ".exe")
            {
                StatuText = "下载完成，正在启动更新.....";

                //更新启动文件
                runFilePath = filePath;
            }
            return runFilePath;
        }

    }
}