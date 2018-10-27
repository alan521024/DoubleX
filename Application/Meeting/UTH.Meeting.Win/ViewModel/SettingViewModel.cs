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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using MahApps.Metro.Controls;
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
            Speeds.Add(new KeyValueModel<string, int>("低速", 1));
            Speeds.Add(new KeyValueModel<string, int>("中速", 5));
            Speeds.Add(new KeyValueModel<string, int>("高速", 10));

            Loading();
        }

        /// <summary>
        /// 源语言列表
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
        /// 目标语言列表
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
        /// 语速列表
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
        /// 源语言
        /// </summary>
        public KeyValueModel Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged(() => Source);
                LangSelectSync();
            }
        }
        private KeyValueModel _source;

        /// <summary>
        /// 目标语言
        /// </summary>
        public string TargetValue
        {
            get { return _targetValue; }
            set
            {
                _targetValue = value;
                RaisePropertyChanged(() => TargetValue);
            }
        }
        private string _targetValue;

        /// <summary>
        /// 语速
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
        /// 字体
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
        /// 会议用户配置数据
        /// </summary>
        public MeetingSettingModel DataModel { get; set; }

        /// <summary>
        /// 保存事件
        /// </summary>
        public ICommand OnSaveCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Save();
                });
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private void Loading()
        {
            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingProfileLoginAccountGet.GetResult<MeetingProfileDTO, MeetingProfileEditInput>();
            if (result.Code != EnumCode.成功)
            {
                throw new DbxException(EnumCode.提示消息, result.Message);
            }

            //设置选择值
            TargetValue = result.Obj.TargetLangs;
            Source = Sources.Where(x => x.Value == result.Obj.SourceLang).FirstOrDefault();
            Speed = Speeds.Where(x => x.Value == result.Obj.Speed).FirstOrDefault();
            FontSize = result.Obj.FontSize;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            var meetingId = Guid.Empty;
            var meetingViewModel = WpfHelper.GetViewModel<MeetingViewModel>();
            if (!meetingViewModel.IsNull() && !meetingViewModel.Meeting.IsNull())
            {
                meetingId = meetingViewModel.Meeting.Id;
            }

            TargetValue = StringHelper.Get(Targets.Where(x => x.IsSelected).Select(x => x.Lang).ToArray(), separator: "|");

            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingProfileLoginAccountSave.GetResult<MeetingProfileDTO, MeetingProfileEditInput>(new MeetingProfileEditInput()
            {
                MeetingId = meetingId,
                SourceLang = Source.Value,
                TargetLangs = TargetValue,
                Speed = Speed.Value,
                FontSize = FontSize,
            });
            if (result.Code != EnumCode.成功)
            {
                MessageAlert(result.Message ?? culture.Lang.sysBaoCunShiBai);
                return;
            }

            MessageAlert(culture.Lang.sysBaoCunChengGong);
        }

        /// <summary>
        /// 源/目标语音选择同步
        /// </summary>
        private void LangSelectSync()
        {
            var values = StringHelper.GetToArray(TargetValue, new string[] { "|" }).ToList();
            Targets.ToList().ForEach(i =>
            {
                i.IsEnable = true;
                i.IsSelected = false;

                if (values.Contains(i.Lang))
                {
                    i.IsSelected = true;
                }
                if (i.Lang == Source.Value)
                {
                    i.IsEnable = false;
                    i.IsSelected = false;
                }
            });
            RaisePropertyChanged(() => Targets);
            TargetValue = StringHelper.Get(Targets.Where(x => x.IsSelected).Select(x => x.Lang).ToArray(), separator: "|");
        }

        /// <summary>
        /// 修改字体
        /// </summary>
        public void ChangeMainUIFontSize()
        {
            var meeting = WpfHelper.GetViewModel<MeetingViewModel>();
            if (!meeting.IsNull())
            {
                meeting.RecordFontSize = FontSize;
            }
        }
    }
}