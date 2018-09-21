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
    using CommonServiceLocator;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    public class SettingViewModel : UTHViewModel
    {
        public SettingViewModel() : base(culture.Lang.sysSheZhi, "")
        {
            Initialize();
        }

        #region ˽�б���

        #endregion

        #region ��������

        /// <summary>
        /// Դ�����б�
        /// </summary>
        public ObservableCollection<KeyValueModel> Sources
        {
            get { return _sources; }
            set
            {
                _sources = value;
                RaisePropertyChanged(() => Sources);
            }
        }
        private ObservableCollection<KeyValueModel> _sources;

        /// <summary>
        /// Դ����ѡ����
        /// </summary>
        public KeyValueModel Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged(() => Source);
                SyncLangSelect();
                RaisePropertyChanged(() => Targets);
            }
        }
        private KeyValueModel _source;

        /// <summary>
        /// Ŀ�������б�
        /// </summary>
        public ObservableCollection<TargetLangObservable> Targets
        {
            get { return _targets; }
            set
            {
                _targets = value; RaisePropertyChanged(() => Targets);
            }
        }
        private ObservableCollection<TargetLangObservable> _targets;

        /// <summary>
        /// �����б�
        /// </summary>
        public ObservableCollection<KeyValueModel<string, int>> Speeds
        {
            get { return _speeds; }
            set
            {
                _speeds = value; RaisePropertyChanged(() => Speeds);
            }
        }
        private ObservableCollection<KeyValueModel<string, int>> _speeds;

        /// <summary>
        /// ����ѡ����
        /// </summary>
        public KeyValueModel<string, int> Speed
        {
            get { return _speed; }
            set
            {
                _speed = value; RaisePropertyChanged(() => Speed);
            }
        }
        private KeyValueModel<string, int> _speed;

        /// <summary>
        /// �����С
        /// </summary>I would rather use [RegionMemberLifetime(KeepAlive = false)] than creating a new property in my view model ;) 
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged(() => FontSize);
            }
        }
        private int _fontSize;

        /// <summary>
        /// �����û���������
        /// </summary>
        public MeetingSettingModel DataModel { get; set; }
        
        #endregion

        #region ��������

        private void Initialize()
        {
            //��ʼѡ����
            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingProfileLoginAccountGet.GetResult<MeetingProfileOutput, MeetingProfileEditInput>();
            if (result.Code != EnumCode.�ɹ�)
            {
                throw new DbxException(EnumCode.��ʾ��Ϣ, result.Message);
            }

            DataModel = EngineHelper.Map<MeetingSettingModel>(result.Obj);

            Sources = new ObservableCollection<KeyValueModel>();
            AppHelper.MeetingSourceLangs.ForEach(i =>
            {
                Sources.Add(i);
            });

            Targets = new ObservableCollection<TargetLangObservable>();
            AppHelper.MeetingTargetLangs.ForEach(i =>
            {
                Targets.Add(new TargetLangObservable() { Text = i.Key, Lang = i.Value, IsSelected = false, IsEnable = false });
            });

            Speeds = new ObservableCollection<KeyValueModel<string, int>>();
            Speeds.Add(new KeyValueModel<string, int>("����", 1));
            Speeds.Add(new KeyValueModel<string, int>("����", 5));
            Speeds.Add(new KeyValueModel<string, int>("����", 10));

            //����ѡ��ֵ
            Source = Sources.Where(x => x.Value == DataModel.SourceLang).FirstOrDefault();
            Speed = Speeds.Where(x => x.Value == DataModel.Speed).FirstOrDefault();
            FontSize = DataModel.FontSize;
        }

        private void SyncLangSelect()
        {
            var tagSelectValues = StringHelper.GetToArray(DataModel.TargetLangs, new string[] { "|" }).ToList();
            Targets.ToList().ForEach(i =>
            {
                i.IsEnable = true;

                var obj = tagSelectValues.Where(x => x == i.Lang).FirstOrDefault();
                i.IsSelected = !obj.IsNull();

                if (i.Lang == Source.Value)
                {
                    i.IsSelected = false;
                    i.IsEnable = false;
                }
            });
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Save(Guid id)
        {
            var input = new MeetingProfileEditInput()
            {
                SourceLang = Source.Value,
                TargetLangs = StringHelper.Get(Targets.Where(x => x.IsSelected).Select(x => x.Lang).ToArray(), separator: "|"),
                Speed = Speed.Value,
                FontSize = FontSize,
                MeetingId = id,
            };

            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingProfileLoginAccountSave.GetResult<MeetingProfileOutput, MeetingProfileEditInput>(input);
            if (result.Code != EnumCode.�ɹ�)
            {
                return result.Message;
            }

            DataModel = EngineHelper.Map<MeetingSettingModel>(result.Obj);

            return string.Empty;
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        public void ChangeMainUIFontSize()
        {
            //WpfHelper.GetViewModel<MainViewModel>().RecordFontSize = FontSize;
        }
    }
}