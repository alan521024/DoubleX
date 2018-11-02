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

        /// <summary>
        /// 最新版本
        /// </summary>
        public string NewsVersion { get { return $"{ culture.Lang.sysZuiXinBanBeng} : {AppHelper.MainApp.Versions.No}"; } }

        /// <summary>
        /// 当前版本
        /// </summary>
        public string CuerrentVersion { get { return $"{culture.Lang.sysDangQianBanBeng} : {AppHelper.MainAppVersion}"; } }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionDescript { get { return AppHelper.MainApp.Versions.Descript; } }

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
        /// 状态内容
        /// </summary>
        public string StatusTip
        {
            get { return _statusTip; }
            set { _statusTip = value; RaisePropertyChanged(() => StatusTip); }
        }
        private string _statusTip;

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
        /// 是否显示
        /// </summary>
        public Visibility ProgressVisable
        {
            get { return _progressVisable; }
            set { _progressVisable = value; RaisePropertyChanged(() => ProgressVisable); }
        }
        private Visibility _progressVisable = Visibility.Collapsed;

        /// <summary>
        /// 更新事件
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
        /// 初始信息
        /// </summary>
        private void Initialize()
        {
            //TODO:Descript 考虑显示html ,https://www.cnblogs.com/LemonFive/p/7801471.html
            IsUpdate = AppHelper.MainApp.Versions.No != AppHelper.MainAppVersion;
            if (!IsUpdate)
            {
                StatusTip = culture.Lang.synDangQianWeiZuiXinBanBeng;
            }
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        private void AppUpdate()
        {
            if (AppHelper.MainApp.Versions.No == AppHelper.MainAppVersion)
            {
                return;
            }

            ProgressValue = 0;
            StatusTip = "准备更新，正在关闭进程";

            //杀掉相关进程
            var killIds = StringHelper.GetToArray(AppHelper.MainAppProcessIds, separator: new string[] { "|" })
                .Select(x => IntHelper.Get(x)).ToArray();
            if (killIds.Length > 0)
            {
                ProcessHelper.Kill(killIds);
            }

            //保存地址
            var file = FilesHelper.GetFile("../Assets/Update", AppHelper.MainApp.Versions.FileName);
            if (file.Exists)
            {
                file.Delete();
            }

            //下载文件
            string downUrl = $"{AppHelper.MainApp.DownloadUrl}?md5={AppHelper.MainApp.Versions.FileMd5}&name={AppHelper.MainApp.Versions.FileName}";
            FilesHelper.DownloadByHttpAsync(downUrl, null, null, file.FullName, (speed, pro) =>
            {
                StatusTip = string.Format(" {0}%  下载速度 {1} KB/S", pro, speed);
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
        /// 解压/复制 下载文件
        /// </summary>
        private string ExtractFile(FileInfo file)
        {
            string runFilePath = string.Empty;

            //解压更新包后运行程序
            if (file.Extension.ToLower() == ".zip")
            {
                //解压原文件 及 解压目标目录
                StatusTip = "下载完成，正在解压文件.....";
                var extDir = new DirectoryInfo($"{ Path.GetTempPath()}/{Guid.NewGuid()}");
                FilesHelper.ExtractFile(file.FullName, extDir.FullName);

                //替换文件
                StatusTip = "解压成功，正在更新.....";
                FilesHelper.CopyFold(extDir.FullName, Path.GetDirectoryName(AppHelper.MainAppPath));

                //TODO:更新时考虑附带一个删除列表(用于删除无用文件)

                //TODO:验证更新是否成功
                //原则是多个文件都需验证(暂只判断是否存在AppHelper.UpdateAppPath传入的可执行文件)

                StatusTip = "更新完成，正在启动.....";
                runFilePath = AppHelper.MainAppPath;
            }

            //直接执行更新包程序
            if (file.Extension.ToLower() == ".exe")
            {
                StatusTip = "下载完成，正在启动更新.....";
                runFilePath = file.FullName;
            }

            //为空时默认启动
            if (runFilePath.IsEmpty())
            {
                runFilePath = AppHelper.MainAppPath;
            }

            return runFilePath;
        }

        /// <summary>
        /// 运行新版
        /// </summary>
        private void AppRun(string runFile)
        {
            IsUpdate = true;
            ProgressVisable = Visibility.Collapsed;

            if (!runFile.IsEmpty())
            {
                //启动文件
                ProcessHelper.Start(runFile, args: "", style: ProcessWindowStyle.Normal);

                //关闭更新应用程序
                WpfHelper.ExcuteUI(() =>
                {
                    Thread.Sleep(500);
                    ((View.Main)DependencyObj).Close();
                });
            }
            else
            {
                throw new DbxException(EnumCode.提示消息, "更新错误");
            }
        }
    }
}