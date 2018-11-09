namespace UTH.Meeting.Win.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using System.Windows.Input;
    using Newtonsoft.Json.Linq;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using NAudio.Wave;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    /// <summary>
    /// 主界面
    /// </summary>
    public class MainViewModel : UTHViewModel
    {
        public MainViewModel() : base(culture.Lang.winZhuJieMian, "")
        {
            VersionInfo();
        }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescript
        {
            get { return _statusDescript; }
            set
            {
                _statusDescript = value;
                RaisePropertyChanged(() => StatusDescript);
            }
        }
        private string _statusDescript;

        /// <summary>
        /// 更新简介
        /// </summary>
        public string UpdateIntro
        {
            get { return _updateIntro; }
            set
            {
                _updateIntro = value;
                RaisePropertyChanged(() => UpdateIntro);
            }
        }
        private string _updateIntro;

        /// <summary>
        /// 是否更新
        /// </summary>
        public Visibility IsUpdate { get; private set; }

        /// <summary>
        /// 是否组织账号
        /// </summary>
        public Visibility IsOrganize { get { return CurrentUser.User.Type == EnumAccountType.组织 ? Visibility.Visible : Visibility.Collapsed; } }


        /// <summary>
        /// 导出事件
        /// </summary>
        public ICommand OnExportCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Export();
                });
            }
        }

        /// <summary>
        /// 导出记录
        /// </summary>
        /// <param name="id"></param>
        public void Export(Guid? meetingId = null)
        {
            if (meetingId.IsEmpty())
            {
                var meetingViewModel = WpfHelper.GetViewModel<MeetingViewModel>();
                if (meetingViewModel.IsNull() || (!meetingViewModel.IsNull() && meetingViewModel.Meeting.IsNull())
                     || (!meetingViewModel.IsNull() && !meetingViewModel.Meeting.IsNull() && meetingViewModel.Meeting.Id.IsEmpty()))
                {
                    throw new DbxException(EnumCode.提示消息, culture.Lang.metWeiKaiShiHuiYi);
                }
                meetingId = meetingViewModel.Meeting.Id;
            }

            StreamWriter file = null;
            try
            {
                this.MaskShow("正在导出....");
                var session = EngineHelper.Resolve<IApplicationSession>() as DefaultSession;
                var res = MeetingHelper.GetRecordService(meetingId.Value);
                var trs = MeetingHelper.GetTranslateService(meetingId.Value);

                string filePath = MeetingHelper.GetMeetingTextFile(meetingId.Value);
                if (File.Exists(filePath))
                {
                    FilesHelper.DeleteFile(filePath);
                }

                var records = res.Find(sorting: new List<KeyValueModel>() { new KeyValueModel("CreateDt", "Asc") });

                using (file = new StreamWriter(filePath, true))
                {
                    records.ToList().ForEach(record =>
                    {
                        file.WriteLine(string.Format("{0}  ({1}){2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            record.Langue,
                            record.Content));
                        var translations = trs.Find(where: t => t.RecordId == record.Id);
                        translations.ForEach(translation =>
                        {
                            file.WriteLine(string.Format("        ({0}){1}", translation.Langue, translation.Content));
                        });
                    });
                }
                System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(MeetingHelper.GetMeetingTextFile(meetingId.Value)));
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.提示消息, ex);
            }
            finally
            {
                file?.Close();
                file?.Dispose();
                file = null;
                this.MaskHide();
            }
        }

        private void VersionInfo()
        {
            var currentVersion = VersionHelper.Get();

            UpdateIntro = string.Format("{0}:v{1} [{2}]",
                culture.Lang.sysDangQianBanBeng,
                currentVersion,
                BoolHelper.Get(AppHelper.Licenses.IsTrial) ? culture.Lang.sysShiYongBan : culture.Lang.sysZhengShiBan);

            IsUpdate = currentVersion.ToString() != AppHelper.CurrentApp.Versions.No ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}