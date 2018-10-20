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
            Initialize();
        }

        private MeetingSettingModel setting
        {
            get
            {
                //单列，应用程序唯一(Setting 界面修改后，不需要重新读取数据库)
                var value = ServiceLocator.Current.GetInstance<SettingViewModel>().DataModel;
                value.Rate = 16000;
                value.Channel = 1;
                value.BitDepth = 16;
                value.BufferMilliseconds = 150;
                value.SentenceMilliseconds = 3500;
                value.RemoteAddress = "ipc://channel/ServerRemoteObject.rem";
                value.ByteLength = MultimediaHelper.GetAudioByteLength(value.Rate, value.BitDepth, value.BufferMilliseconds);
                return value;
            }
        }
        private IRecorderService<WaveInCapabilities> recorder { get; set; }
        private ServerMarshalByRefObject server
        {
            get
            {
                if (_server.IsNull())
                {
                    _server = AppHelper.GetServerMarshalByRefObject();
                }
                if (_server.IsNull())
                {
                    EngineHelper.LoggingError($"server is null");
                    taskException?.Cancel();
                }
                try
                {
                    var check = _server.IsConnection;
                }
                catch (Exception ex)
                {
                    EngineHelper.LoggingError(ex);
                    taskException.Cancel();
                }
                return _server;
            }
        }
        private ServerMarshalByRefObject _server;
        private CancellationTokenSource taskException = new CancellationTokenSource();
        private CancellationTokenSource taskCancel = new CancellationTokenSource();

        
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
        /// 麦克风音量
        /// </summary>
        public float MicrophoneVolume
        {
            get { return _microphoneVolume; }
            set { _microphoneVolume = value; RaisePropertyChanged(() => MicrophoneVolume); }
        }
        private float _microphoneVolume = 0;

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
        /// 会议信息
        /// </summary>
        public MeetingDTO Meeting
        {
            get { return _meeting; }
            set { _meeting = value; RaisePropertyChanged(() => Meeting); }
        }
        private MeetingDTO _meeting;

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
        /// 加载会议
        /// </summary>
        /// <param name="code"></param>
        public void Loading(string code = null, MeetingDTO meeting = null)
        {
            //加载/创建
            if (code.IsEmpty() && meeting.IsNull())
            {
                Meeting.Id = Guid.Empty;
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingInsert.GetResult<MeetingDTO, MeetingEditInput>(EngineHelper.Map<MeetingEditInput>(Meeting));
                if (result.Code == EnumCode.成功)
                {
                    Meeting = result.Obj;
                }
            }
            else if (!code.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetCode.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput() { Num = code });
                if (result.Code == EnumCode.成功)
                {
                    Meeting = result.Obj;
                }
            }
            else if (!meeting.IsNull())
            {
                Meeting = meeting;
            }

            //会议校验
            if (Meeting.Id.IsEmpty() || Meeting.Num.IsEmpty())
            {
                throw new DbxException(EnumCode.初始失败);
            }

            //二维码
            var codeBitmap = QrCodeHelper.GetCode(string.Format(EngineHelper.Configuration.Settings.GetValue("meetingViewUrl"), Meeting.Id));
            MeetingCode = WpfHelper.BitmapToSource(codeBitmap);

            //本地数据库
            var meetingDatabasePath = MeetingHelper.GetMeetingDatabaseFile(Meeting.Id);
            if (!File.Exists(meetingDatabasePath))
            {
                FilesHelper.CopyFile(MeetingHelper.TemplateDatabaseFilePath, meetingDatabasePath);
            }

            //远程初始
            server.Initialize(Meeting, Session.Accessor.Token);

            //UI状态
            SyncUIStatus(EnumTaskStatus.Init);

            //同步任务
            taskException.Token.Register(() =>
            {
                taskCancel?.Cancel();
                throw new DbxException(EnumCode.服务异常);
            });
            taskCancel.Token.Register(() =>
            {
            });
            CancellationTokenSource compositeCancel = CancellationTokenSource.CreateLinkedTokenSource(taskException.Token, taskCancel.Token);
            compositeCancel.Token.Register(() =>
            {
                SyncUIStatus(EnumTaskStatus.Stoped);
                SyncUIStatus(EnumTaskStatus.Clear);
            });
            Task.Factory.StartNew(() =>
            {
                while (true && !compositeCancel.IsCancellationRequested)
                {
                    ThreadPool.QueueUserWorkItem((Object state) =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            try
                            {
                                SyncData();
                            }
                            catch (Exception ex)
                            {
                                EngineHelper.LoggingError(ex);
                                compositeCancel.Cancel();
                            }
                        });
                    });
                    Thread.Sleep(10);
                }
            }, compositeCancel.Token);
        }

        /// <summary>
        /// 开始会议
        /// </summary>
        public void Start()
        {
            Meeting.CheckNull();
            Microphone.CheckNull();

            recorder.Configruation((opt) =>
            {
                opt.DeviceNum = Microphone.Key;
                opt.Rate = setting.Rate;
                opt.BitDepth = setting.BitDepth;
                opt.Channel = setting.Channel;
                opt.BufferMilliseconds = setting.BufferMilliseconds;
                opt.DataEvent = (sender, e) =>
                {
                    server.MeetingSend(Meeting.Id, e.Buffer, 0, e.BytesRecorded);
                };
                opt.VolumeEvent = (volume) =>
                {
                    MicrophoneVolume = volume;
                };
                opt.StopedEvent = (sender, e) =>
                {
                    Trace.WriteLine($"stop: {DateTime.Now}");
                };

                opt.FileName = MeetingHelper.GetMeetingWavFile(Meeting.Id);

            });
            recorder.Start();

            server.Configuration(setting);
            server.MeetingStart();

            SyncUIStatus(EnumTaskStatus.Started);
        }

        /// <summary>
        /// 停止会议
        /// </summary>
        public void Stop()
        {
            recorder?.Stop();
            server?.MeetingStop();
            SyncUIStatus(EnumTaskStatus.Stoped);
        }

        /// <summary>
        /// 清除记录
        /// </summary>
        public void Clear()
        {
            SyncUIStatus(EnumTaskStatus.Clear);
        }

        /// <summary>
        /// 取消会议
        /// </summary>
        public void Cancel()
        {
            recorder?.Stop();
            //server?.MeetingStop();
            taskCancel?.Cancel();
        }


        private void Initialize()
        {
            //UI状态
            SyncUIStatus(EnumTaskStatus.Default);

            //会议初始
            Meeting = new MeetingDTO()
            {
                Id = Guid.Empty,
                Name = culture.Lang.metName,
                Descript = culture.Lang.metDescript,
                Setting = JsonHelper.Serialize(setting)
            };

            //麦克风列表
            Microphones = RecordingHelper.Microphones();
            Microphone = Microphones.FirstOrDefault();

            //记录字体
            RecordFontSize = setting.FontSize;

            //录音器
            recorder = new RecorderWaveInService2();
        }
        private void SyncData()
        {
            //本地最后一条数据,空段xx秒后 处理
            var last = Records.Where(x => !x.IsComplete).LastOrDefault();
            if (!last.IsNull())
            {
                if (DateTime.Now > last.RefreshDt.AddMilliseconds(setting.SentenceMilliseconds))
                {
                    last.IsComplete = true;
                }
                if (last.IsComplete)
                {
                    if (last.Content.Length > 0 && !StringHelper.Punctuations.Contains(last.Content.Substring(last.Content.Length - 1, 1)))
                    {
                        last.Content = last.Content + "。";
                    }
                    if (last.Content.IsEmpty())
                    {
                        Records.Remove(last);
                    }
                    else
                    {
                        server.MeetingInsert(last.LocalId, last.MeetingId, last.Langue, last.LangueTrs, last.Content);
                    }
                }
            }

            var model = server.MeetingSync();

            if (model.IsNull())
                return;

            if (model.Content.TrimStartPunctuation().IsEmpty())
                return;

            if (model.SyncType <= 0)
            {
                var content = model.Content.TrimStartPunctuation().TrimEndPunctuation();
                var record = Records.Where(x => !x.IsComplete).LastOrDefault();

                if (record.IsNull() && !content.IsEmpty())
                {
                    record = new RecordObservable();
                    record.Id = Guid.Empty;
                    record.LocalId = Guid.NewGuid();
                    record.MeetingId = Meeting.Id;
                    record.Langue = setting.SourceLang;
                    record.LangueTrs = setting.TargetLangs;
                    record.Translations = new ObservableCollection<TranslationObservable>();
                    Records.Add(record);
                }

                if (record.IsNull())
                    return;

                if (!record.Content.IsEmpty() && !StringHelper.Punctuations.Contains(model.Content.Substring(0, 1)))
                {
                    record.Content = record.Content + "，";
                }
                if (record.Content.IsEmpty())
                {
                    record.Content = model.Content.TrimStartPunctuation();
                }
                else
                {
                    record.Content = record.Content + model.Content;
                }

                record.IsComplete = model.SyncType == 0; //-1:当前句子未完,0：当交句子为最后完成
                record.RefreshDt = DateTime.Now;

                if (record.IsComplete)
                {
                    if (record.Content.IsEmpty())
                    {
                        Records.Remove(record);
                    }
                    else
                    {
                        server.MeetingInsert(record.LocalId, record.MeetingId, record.Langue, record.LangueTrs, record.Content);
                    }
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

            SyncUIStatus(EnumTaskStatus.Loading);
        }
        private void SyncUIStatus(EnumTaskStatus status)
        {
            switch (status)
            {
                case EnumTaskStatus.Default:

                    CanStart = false;
                    CanStop = false;
                    CanClear = false;
                    IsRecords = Visibility.Collapsed;
                    Records = new ObservableCollection<RecordObservable>();

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

    }
}