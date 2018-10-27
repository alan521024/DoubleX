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
    /// ���ֻ�����Ϣ
    /// </summary>
    [Serializable]
    public class MeetingViewModel : UTHViewModel
    {
        public MeetingViewModel() : base(culture.Lang.metHuiYiShi, "")
        {
        }

        /// <summary>
        /// ������Ϣ
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
        /// ��������
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
        /// �����ά��
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
        /// ��˷�����
        /// </summary>
        public float MicrophoneVolume
        {
            get { return _microphoneVolume; }
            set { _microphoneVolume = value; RaisePropertyChanged(() => MicrophoneVolume); }
        }
        private float _microphoneVolume = 0;

        /// <summary>
        /// ��˷��б�
        /// </summary>
        public List<KeyValueModel<int, WaveInCapabilities>> Microphones
        {
            get { return _microphones; }
            set { _microphones = value; RaisePropertyChanged(() => Microphones); }
        }
        private List<KeyValueModel<int, WaveInCapabilities>> _microphones = new List<KeyValueModel<int, WaveInCapabilities>>();

        /// <summary>
        /// ѡ�����˷�
        /// </summary>
        public KeyValueModel<int, WaveInCapabilities> Microphone
        {
            get { return _microphone; }
            set { _microphone = value; RaisePropertyChanged(() => Microphone); }
        }
        private KeyValueModel<int, WaveInCapabilities> _microphone;

        /// <summary>
        /// �Ƿ�ʼ
        /// </summary>
        public bool CanStart
        {
            get { return _canStart; }
            set { _canStart = value; RaisePropertyChanged(() => CanStart); }
        }
        private bool _canStart = false;

        /// <summary>
        /// �Ƿ�ֹͣ
        /// </summary>
        public bool CanStop
        {
            get { return _canStop; }
            set { _canStop = value; RaisePropertyChanged(() => CanStop); }
        }
        private bool _canStop = false;

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool CanClear
        {
            get { return _canClear; }
            set { _canClear = value; RaisePropertyChanged(() => CanClear); }
        }
        private bool _canClear = false;

        /// <summary>
        /// �Ƿ���ʾ��¼
        /// </summary>
        public Visibility IsRecords
        {
            get { return _isRecords; }
            set { _isRecords = value; RaisePropertyChanged(() => IsRecords); }
        }
        private Visibility _isRecords = Visibility.Collapsed;

        /// <summary>
        /// ��¼�ֺ�
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
        /// �����¼
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
        private bool IsRecording { get; set; } //�������������仯�ж��Ƿ���¼����

        /// <summary>
        /// ���ػ���
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
                if (result.Code == EnumCode.�ɹ�)
                {
                    Meeting = meeting;
                }
            }
            else if (Meeting.IsNull())
            {
                //else if(Meeting.IsNull()) ȫ�ֻ��鱣��
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingInsert.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput()
                {
                    Id = Guid.Empty,
                    Name = culture.Lang.metName,
                    Descript = culture.Lang.metDescript,
                    Setting = JsonHelper.Serialize(Setting)
                });
                if (result.Code == EnumCode.�ɹ�)
                {
                    Meeting = result.Obj;
                }
            }
            if (Meeting.IsNull() || (!Meeting.IsNull() && (Meeting.Id.IsEmpty() || Meeting.Num.IsEmpty())))
            {
                throw new DbxException(EnumCode.��ʼʧ��);
            }

            //device
            Microphones = RecordingHelper.Microphones();
            Microphone = Microphones.FirstOrDefault();

            //��ά��
            var codeBitmap = QrCodeHelper.GetCode(string.Format(EngineHelper.Configuration.Settings.GetValue("meetingViewUrl"), Meeting.Id));
            MeetingCode = WpfHelper.BitmapToSource(codeBitmap);

            //�������ݿ�
            var meetingDatabasePath = MeetingHelper.GetMeetingDatabaseFile(Meeting.Id);
            if (!File.Exists(meetingDatabasePath))
            {
                FilesHelper.CopyFile(MeetingHelper.TemplateDatabaseFilePath, meetingDatabasePath);
            }

            //��¼����
            RecordFontSize = Setting.FontSize;

            //ͬ������
            SyncTask();

            //ͬ��״̬
            SyncUI(EnumTaskStatus.Default);
            SyncUI(EnumTaskStatus.Init);

            //Զ�̷���ͬ������
            AppHelper.ServerObj.MeetingSyncTask();
        }

        /// <summary>
        /// ��ʼ����
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
        /// ֹͣ����
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
        /// �������
        /// </summary>
        public void Clear()
        {
            SyncUI(EnumTaskStatus.Clear);
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        public void Cancel()
        {
            syncTaskCancel?.Cancel();
        }


        /// <summary>
        /// ͬ������
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
                                    throw new DbxException(EnumCode.��ʾ��Ϣ, "ͬ������(����)");
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
        /// ͬ��UI״̬
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
            //û������[������С]����10����ж����һ������
            if ((DateTime.Now - PreVolDt).TotalMilliseconds > Setting.SentenceMilliseconds)
            {
                Records.Where(x => !x.IsComplete).ToList().ForEach(item =>
                {
                    if (!StringHelper.Punctuations.Contains(item.Content.Substring(item.Content.Length - 1, 1)))
                    {
                        item.Content = item.Content + (Setting.SourceLang == "zs" ? "��" : ".");
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
                if (result.Code == EnumCode.�ɹ�)
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