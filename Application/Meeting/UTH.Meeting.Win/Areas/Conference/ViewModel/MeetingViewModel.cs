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
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using NAudio.Wave;
    using CommonServiceLocator;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;
    using UTH.Meeting.Server;

    /// <summary>
    /// 主持会议信息
    /// </summary>
    [Serializable]
    public class MeetingViewModel : UTHViewModel
    {
        public MeetingViewModel() : base(culture.Lang.metHuiYiShi, "")
        {
        }

        /// <summary>
        /// 会议信息
        /// </summary>
        public MeetingDTO Meeting
        {
            get { return _meeting; }
            set
            {
                _meeting = value; RaisePropertyChanged(() => Meeting);
            }
        }
        private MeetingDTO _meeting = null;

        /// <summary>
        /// 会议配置
        /// </summary>
        public MeetingSettingModel Setting
        {
            get
            {
                if (_seeting.IsNull())
                {
                    _seeting = new MeetingSettingModel();
                }

                _seeting.Rate = 16000;
                _seeting.Channel = 1;
                _seeting.BitDepth = 16;
                _seeting.BufferMilliseconds = 150;
                _seeting.SentenceMilliseconds = IntHelper.Get(EngineHelper.Configuration.Settings.GetValue("sentenceMilliseconds"), 3000);
                _seeting.RemoteAddress = "ipc://channel/ServerRemoteObject.rem";
                _seeting.ByteLength = MultimediaHelper.GetAudioByteLength(_seeting.Rate, _seeting.BitDepth, _seeting.BufferMilliseconds);

                _seeting.SourceLang = seetingViewModel.Source.Value;
                _seeting.TargetLangs = StringHelper.Get(seetingViewModel.Targets.Where(x => x.IsSelected).Select(x => x.Lang).ToArray(), separator: "|");
                _seeting.Speed = seetingViewModel.Speed.Value;
                _seeting.FontSize = seetingViewModel.FontSize;

                return _seeting;
            }
        }
        private MeetingSettingModel _seeting;

        /// <summary>
        /// 会议二维码
        /// </summary>
        public BitmapSource MeetingCode
        {
            get { return _meetingCode; }
            set
            {
                _meetingCode = value; RaisePropertyChanged(() => MeetingCode);
            }
        }
        private BitmapSource _meetingCode = null;


        /// <summary>
        /// 麦克风音量
        /// </summary>
        public float MicrophoneVolume
        {
            get { return _microphoneVolume; }
            set { _microphoneVolume = value; RaisePropertyChanged(() => MicrophoneVolume); }
        }
        private float _microphoneVolume = 0;

        /// <summary>
        /// 麦克风列表
        /// </summary>
        public List<KeyValueModel<int, WaveInCapabilities>> Microphones
        {
            get { return _microphones; }
            set { _microphones = value; RaisePropertyChanged(() => Microphones); }
        }
        private List<KeyValueModel<int, WaveInCapabilities>> _microphones = new List<KeyValueModel<int, WaveInCapabilities>>();

        /// <summary>
        /// 选择的麦克风
        /// </summary>
        public KeyValueModel<int, WaveInCapabilities> Microphone
        {
            get { return _microphone; }
            set { _microphone = value; RaisePropertyChanged(() => Microphone); }
        }
        private KeyValueModel<int, WaveInCapabilities> _microphone;

        /// <summary>
        /// 是否开始
        /// </summary>
        public bool CanStart
        {
            get { return _canStart; }
            set { _canStart = value; RaisePropertyChanged(() => CanStart); }
        }
        private bool _canStart = false;

        /// <summary>
        /// 是否停止
        /// </summary>
        public bool CanStop
        {
            get { return _canStop; }
            set { _canStop = value; RaisePropertyChanged(() => CanStop); }
        }
        private bool _canStop = false;

        /// <summary>
        /// 是否可以清除
        /// </summary>
        public bool CanClear
        {
            get { return _canClear; }
            set { _canClear = value; RaisePropertyChanged(() => CanClear); }
        }
        private bool _canClear = false;

        /// <summary>
        /// 是否显示记录
        /// </summary>
        public Visibility IsRecords
        {
            get { return _isRecords; }
            set { _isRecords = value; RaisePropertyChanged(() => IsRecords); }
        }
        private Visibility _isRecords = Visibility.Collapsed;

        /// <summary>
        /// 记录字号
        /// </summary>
        public int RecordFontSize
        {
            get { return _recordFontSize; }
            set
            {
                _recordFontSize = value;
                RaisePropertyChanged(() => RecordFontSize);
            }
        }
        private int _recordFontSize;

        /// <summary>
        /// 会议记录
        /// </summary>
        public ObservableCollection<RecordObservable> Records
        {
            get { return _records; }
            set
            {
                _records = value;
                RaisePropertyChanged(() => Records);
            }
        }
        private ObservableCollection<RecordObservable> _records = new ObservableCollection<RecordObservable>();


        private SettingViewModel seetingViewModel = WpfHelper.GetViewModel<SettingViewModel>();
        private Task syncTask = null;
        private CancellationTokenSource syncTaskCancel = null;
        private ServerMarshalByRefObject server = AppHelper.ServerObj;
        private IRecorderService<WaveInCapabilities> recorderService = new RecorderWaveInService2();
        private float PreVol = 0;
        private DateTime PreVolDt = DateTime.MinValue;
        private bool IsRecording { get; set; } //根据语音音量变化判断是否在录音中

        /// <summary>
        /// 加载会议
        /// </summary>
        public void Loading(string code = null, MeetingDTO meeting = null)
        {
            //meeting
            if (!meeting.IsNull())
            {
                Meeting = meeting;
            }
            else if (!code.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetCode.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput() { Num = code });
                if (result.Code == EnumCode.成功)
                {
                    Meeting = meeting;
                }
            }
            else if (Meeting.IsNull())
            {
                //else if(Meeting.IsNull()) 全局会议保持
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingInsert.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput()
                {
                    Id = Guid.Empty,
                    Name = culture.Lang.metName,
                    Descript = culture.Lang.metDescript,
                    Setting = JsonHelper.Serialize(Setting)
                });
                if (result.Code == EnumCode.成功)
                {
                    Meeting = result.Obj;
                }
            }
            if (Meeting.IsNull() || (!Meeting.IsNull() && (Meeting.Id.IsEmpty() || Meeting.Num.IsEmpty())))
            {
                throw new DbxException(EnumCode.初始失败);
            }

            //device
            Microphones = RecordingHelper.Microphones();
            Microphone = Microphones.FirstOrDefault();

            //二维码
            var codeBitmap = QrCodeHelper.GetCode(string.Format(EngineHelper.Configuration.Settings.GetValue("meetingViewUrl"), Meeting.Id));
            MeetingCode = WpfHelper.BitmapToSource(codeBitmap);

            //本地数据库
            var meetingDatabasePath = MeetingHelper.GetMeetingDatabaseFile(Meeting.Id);
            if (!File.Exists(meetingDatabasePath))
            {
                FilesHelper.CopyFile(MeetingHelper.TemplateDatabaseFilePath, meetingDatabasePath);
            }

            //记录字体
            RecordFontSize = Setting.FontSize;

            //同步任务
            SyncTask();

            //同步状态
            SyncUI(EnumTaskStatus.Default);
            SyncUI(EnumTaskStatus.Init);

            //远程服务同步任务
            AppHelper.ServerObj.MeetingSyncTask();
        }

        /// <summary>
        /// 开始会议
        /// </summary>
        public void Start()
        {
            //check
            Meeting.CheckNull();
            Microphone.CheckNull();
            recorderService.CheckNull();

            //server[speech]
            server.SpeechStart(Setting);

            //recorder
            recorderService.Configruation((opt) =>
            {
                opt.DeviceNum = Microphone.Key;
                opt.Rate = Setting.Rate;
                opt.BitDepth = Setting.BitDepth;
                opt.Channel = Setting.Channel;
                opt.BufferMilliseconds = Setting.BufferMilliseconds;
                opt.DataEvent = (sender, e) =>
                {
                    //server.MeetingSend(Meeting.Id, e.Buffer, 0, e.BytesRecorded);
                    server.SpeechSend(new Plug.Speech.SpeechData()
                    {
                        Key = Meeting.Id,
                        Data = e.Buffer,
                        Offset = 0,
                        Length = e.BytesRecorded,
                        LastDt = DateTime.Now
                    });
                };
                opt.VolumeEvent = (sender, e, volume) =>
                {
                    MicrophoneVolume = volume;
                    if (PreVol != volume)
                    {
                        PreVolDt = DateTime.Now;
                    }
                    PreVol = volume;
                    //Trace.WriteLine($"v:{volume} / {PreVolDt}  {DateTime.Now - PreVolDt}");
                };
                opt.StopedEvent = (sender, e) =>
                {
                    Trace.WriteLine($"stop: {DateTime.Now}");
                };
                opt.FileName = MeetingHelper.GetMeetingWavFile(Meeting.Id);
            });
            recorderService.Start();

            //task
            SyncTask();

            //sync
            SyncUI(EnumTaskStatus.Started);
        }

        /// <summary>
        /// 停止会议
        /// </summary>
        public void Stop()
        {
            //server[speech]
            server.SpeechStop();

            //recorder
            recorderService?.Stop();

            SyncUI(EnumTaskStatus.Stoped);
        }

        /// <summary>
        /// 清除会议
        /// </summary>
        public void Clear()
        {
            SyncUI(EnumTaskStatus.Clear);
        }

        /// <summary>
        /// 取消会议
        /// </summary>
        public void Cancel()
        {
            syncTaskCancel?.Cancel();
        }


        /// <summary>
        /// 同步任务
        /// </summary>
        private void SyncTask()
        {
            if (syncTask.IsNull() || (!syncTask.IsNull() && (syncTask.IsCompleted || syncTask.IsCanceled)))
            {
                syncTaskCancel = new CancellationTokenSource();
                syncTaskCancel.Token.Register(() =>
                {
                    Stop();
                });

                syncTask = Task.Factory.StartNew(() =>
                {
                    var syncError = 0;
                    while (true && !syncTaskCancel.IsCancellationRequested)
                    {
                        WpfHelper.ExcuteUI(() =>
                        {
                            try
                            {
                                if (syncError > 10)
                                {
                                    syncTaskCancel.Cancel();
                                    throw new DbxException(EnumCode.提示消息, "同步错误(中文)");
                                }
                                SyncLast();
                                SyncData(server.MeetingDataGet(Meeting.Id));
                                SyncUI(EnumTaskStatus.Loading);
                            }
                            catch (Exception ex)
                            {
                                syncError++;
                                EngineHelper.LoggingError(ex);
                            }
                        });
                        Thread.Sleep(10);
                    }
                }, syncTaskCancel.Token);
            }

            if (!(syncTask.Status == TaskStatus.Running || syncTask.Status == TaskStatus.WaitingToRun))
            {
                syncTask.Start();
            }
        }

        /// <summary>
        /// 同步UI状态
        /// </summary>
        /// <param name="status"></param>
        private void SyncUI(EnumTaskStatus status)
        {
            switch (status)
            {
                case EnumTaskStatus.Default:
                    CanStart = false;
                    CanStop = false;
                    CanClear = false;
                    IsRecords = Visibility.Collapsed;
                    break;
                case EnumTaskStatus.Init:
                    CanStart = true;
                    CanStop = false;
                    break;
                case EnumTaskStatus.Started:
                    CanStart = false;
                    CanStop = true;
                    break;
                case EnumTaskStatus.Stoped:
                    CanStart = true;
                    CanStop = false;
                    break;
                case EnumTaskStatus.Clear:
                    CanClear = false;
                    IsRecords = Visibility.Collapsed;
                    Records = new ObservableCollection<RecordObservable>();
                    break;
                case EnumTaskStatus.Loading:
                    if (Records.Count > 0 && IsRecords != Visibility.Visible)
                    {
                        IsRecords = Visibility.Visible;
                        CanClear = true;
                    }
                    if (Records.Count == 0 && IsRecords != Visibility.Collapsed)
                    {
                        IsRecords = Visibility.Collapsed;
                        CanClear = false;
                    }
                    if (Records.Count > 100)
                    {
                        //TODO:RECORDS.COUNT > 100
                    }
                    break;
            }
        }
        
        private void SyncLast()
        {
            //没有语音[声音大小]输入10秒后判断最后一条数据
            if ((DateTime.Now - PreVolDt).TotalMilliseconds > Setting.SentenceMilliseconds)
            {
                Records.Where(x => !x.IsComplete).ToList().ForEach(item =>
                {
                    if (!StringHelper.Punctuations.Contains(item.Content.Substring(item.Content.Length - 1, 1)))
                    {
                        item.Content = item.Content + (Setting.SourceLang == "zs" ? "。" : ".");
                    }
                    item.RefreshDt = DateTime.Now;
                    item.IsComplete = true;
                });
            }

            //add new
            Records.Where(x => x.IsComplete && x.Id.IsEmpty()).ToList().ForEach(item =>
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingRecordCreate.GetResult<List<MeetingSyncModel>, MeetingSyncModel>(new MeetingSyncModel()
                {
                    LocalId = item.LocalId,
                    MeetingId = item.MeetingId,
                    Langue = item.Langue,
                    LangueTrs = item.LangueTrs,
                    Content = item.Content,
                    Sort = 0
                });
                if (result.Code == EnumCode.成功)
                {
                    result.Obj.ForEach(x =>
                    {
                        SyncData(x);
                    });
                }
            });
        }

        private void SyncData(MeetingSyncModel model)
        {
            if (model == null || (model != null && model.Content.IsEmpty()))
                return;

            if (model.SyncType <= 0)
            {
                var last = Records.Where(x => !x.IsComplete).LastOrDefault();
                if (last.IsNull())
                {
                    last = new RecordObservable();
                    last.Id = Guid.Empty;
                    last.LocalId = Guid.Empty;
                    last.IsComplete = false;
                    last.Translations = new ObservableCollection<TranslationObservable>();
                    Records.Add(last);
                }
                last.MeetingId = Meeting.Id;
                last.Langue = Setting.SourceLang;
                last.LangueTrs = Setting.TargetLangs;
                last.Content = last.Content + model.Content;
                last.RefreshDt = DateTime.Now;
                last.IsComplete = model.SyncType == 0;
                if (last.LocalId == Guid.Empty)
                {
                    last.LocalId = Guid.NewGuid();
                    last.Content = last.Content.TrimStartPunctuation();
                }
            }

            if (model.SyncType == 1)
            {
                var record = Records.Where(x => x.LocalId == model.LocalId).FirstOrDefault();
                if (record.IsNull() && !model.RecordId.IsEmpty())
                {
                    record = new RecordObservable();
                    record.IsComplete = true;
                    Records.Add(record);
                }

                if (record.IsNull())
                    return;

                record.Id = model.RecordId;
                record.LocalId = model.LocalId;
                record.MeetingId = model.MeetingId;
                record.Langue = model.Langue;
                record.LangueTrs = model.LangueTrs;
                record.Content = model.Content;
                record.Des = $" -B-";
                record.RefreshDt = model.RefreshDt;
                record.Sort = model.Sort;
                record.SyncType = model.SyncType;
            }

            if (model.SyncType == 2)
            {
                RecordObservable record = Records.Where(x => x.Id == model.RecordId).FirstOrDefault();
                if (record.IsNull())
                    return;

                var translation = record.Translations.FirstOrDefault(x => x.Langue == model.Langue);
                var isAdd = translation.IsNull();
                if (isAdd)
                {
                    translation = new TranslationObservable();
                }

                translation.Id = model.TranslationId;
                translation.MeetingId = model.MeetingId;
                translation.RecordId = model.RecordId;
                translation.Langue = model.Langue;
                translation.Content = model.Content;
                translation.Sort = model.Sort;

                if (isAdd)
                {
                    record.Translations.Add(translation);
                }
            }
        }
    }
}