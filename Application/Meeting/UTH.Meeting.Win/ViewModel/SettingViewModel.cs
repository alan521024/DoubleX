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

        #region 私有变量

        #endregion

        #region 公共属性

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
        /// 源语言选中项
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
        /// 语速选中项
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
        /// 字体大小
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
        
        #endregion

        #region 辅助操作

        private void Initialize()
        {
            //初始选择项
            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingProfileLoginAccountGet.GetResult<MeetingProfileOutput, MeetingProfileEditInput>();
            if (result.Code != EnumCode.成功)
            {
                throw new DbxException(EnumCode.提示消息, result.Message);
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
            Speeds.Add(new KeyValueModel<string, int>("低速", 1));
            Speeds.Add(new KeyValueModel<string, int>("中速", 5));
            Speeds.Add(new KeyValueModel<string, int>("高速", 10));

            //设置选择值
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
        /// 保存配置
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
            if (result.Code != EnumCode.成功)
            {
                return result.Message;
            }

            DataModel = EngineHelper.Map<MeetingSettingModel>(result.Obj);

            return string.Empty;
        }

        /// <summary>
        /// 修改字体
        /// </summary>
        public void ChangeMainUIFontSize()
        {
            //WpfHelper.GetViewModel<MainViewModel>().RecordFontSize = FontSize;
        }
    }
}