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
    /// ������
    /// </summary>
    public class MainViewModel : UTHViewModel
    {
        public MainViewModel() : base(culture.Lang.winZhuJieMian, "")
        {
            Initialize();
        }

        #region ��������

        /// <summary>
        /// ״̬����
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
        /// ���¼��
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
        /// �Ƿ����
        /// </summary>
        public Visibility IsUpdate { get; private set; }

        /// <summary>
        /// �Ƿ���֯�˺�
        /// </summary>
        public Visibility IsOrganize { get { return CurrentUser.User.Type == EnumAccountType.��֯ ? Visibility.Visible : Visibility.Collapsed; } }

        #endregion

        #region ˽�б���

        #endregion

        #region ��������

        private void Initialize()
        {
            var currentVersion = VersionHelper.Get();

            UpdateIntro = string.Format("{0}:v{1} [{2}]",
                culture.Lang.sysDangQianBanBeng,
                currentVersion,
                BoolHelper.Get(AppHelper.Licenses.IsTrial) ? culture.Lang.sysShiYongBan : culture.Lang.sysZhengShiBan);

            IsUpdate = currentVersion != AppHelper.Current.Versions.No ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        /// <summary>
        /// ������¼
        /// </summary>
        /// <param name="id"></param>
        public void Export(Guid id)
        {
            StreamWriter file = null;
            try
            {
                var session = EngineHelper.Resolve<IApplicationSession>() as DefaultSession;
                var res = MeetingHelper.GetLocalRecordServer(id, session);
                var trs = MeetingHelper.GetLocalTranslationServer(id, session);

                string filePath = MeetingHelper.GetMeetingTextFile(id);
                if (File.Exists(filePath))
                {
                    FilesHelper.DeleteFile(filePath);
                }

                var records = res.Query(Sorting: new List<KeyValueModel>() { new KeyValueModel("CreateDt", "Asc") });

                using (file = new StreamWriter(filePath, true))
                {
                    records.ToList().ForEach(record =>
                    {
                        file.WriteLine(string.Format("{0}  ({1}){2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            record.Langue,
                            record.Content));
                        var translations = trs.Query(where: t => t.RecordId == record.Id);
                        translations.ForEach(translation =>
                        {
                            file.WriteLine(string.Format("        ({0}){1}", translation.Langue, translation.Content));
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.��ʾ��Ϣ, ex);
            }
            finally
            {
                file?.Close();
                file?.Dispose();
                file = null;
            }
        }
    }
}