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
            Initialize();
        }

        private MeetingSettingModel setting
        {
            get
            {
                //���У�Ӧ�ó���Ψһ(Setting �����޸ĺ󣬲���Ҫ���¶�ȡ���ݿ�)
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
        /// ��˷�����
        /// </summary>
        public float MicrophoneVolume
        {
            get { return _microphoneVolume; }
            set { _microphoneVolume = value; RaisePropertyChanged(() => MicrophoneVolume); }
        }
        private float _microphoneVolume = 0;

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
        /// ������Ϣ
        /// </summary>
        public MeetingDTO Meeting
        {
            get { return _meeting; }
            set { _meeting = value; RaisePropertyChanged(() => Meeting); }
        }
        private MeetingDTO _meeting;

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
        /// ���ػ���
        /// </summary>
        /// <param name="code"></param>
        public void Loading(string code = null, MeetingDTO meeting = null)
        {
            //����/����
            if (code.IsEmpty() && meeting.IsNull())
            {
                Meeting.Id = Guid.Empty;
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingInsert.GetResult<MeetingDTO, MeetingEditInput>(EngineHelper.Map<MeetingEditInput>(Meeting));
                if (result.Code == EnumCode.�ɹ�)
                {
                    Meeting = result.Obj;
                }
            }
            else if (!code.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetCode.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput() { Num = code });
                if (result.Code == EnumCode.�ɹ�)
                {
                    Meeting = result.Obj;
                }
            }
            else if (!meeting.IsNull())
            {
                Meeting = meeting;
            }

            //����У��
            if (Meeting.Id.IsEmpty() || Meeting.Num.IsEmpty())
            {
                throw new DbxException(EnumCode.��ʼʧ��);
            }

            //��ά��
            var codeBitmap = QrCodeHelper.GetCode(string.Format(EngineHelper.Configuration.Settings.GetValue("meetingViewUrl"), Meeting.Id));
            MeetingCode = WpfHelper.BitmapToSource(codeBitmap);

            //�������ݿ�
            var meetingDatabasePath = MeetingHelper.GetMeetingDatabaseFile(Meeting.Id);
            if (!File.Exists(meetingDatabasePath))
            {
                FilesHelper.CopyFile(MeetingHelper.TemplateDatabaseFilePath, meetingDatabasePath);
            }

            //Զ�̳�ʼ
            server.Initialize(Meeting, Session.Accessor.Token);

            //UI״̬
            SyncUIStatus(EnumTaskStatus.Init);

            //ͬ������
            taskException.Token.Register(() =>
            {
                taskCancel?.Cancel();
                throw new DbxException(EnumCode.�����쳣);
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
        /// ��ʼ����
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
        /// ֹͣ����
        /// </summary>
        public void Stop()
        {
            recorder?.Stop();
            server?.MeetingStop();
            SyncUIStatus(EnumTaskStatus.Stoped);
        }

        /// <summary>
        /// �����¼
        /// </summary>
        public void Clear()
        {
            SyncUIStatus(EnumTaskStatus.Clear);
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        public void Cancel()
        {
            recorder?.Stop();
            //server?.MeetingStop();
            taskCancel?.Cancel();
        }


        private void Initialize()
        {
            //UI״̬
            SyncUIStatus(EnumTaskStatus.Default);

            //�����ʼ
            Meeting = new MeetingDTO()
            {
                Id = Guid.Empty,
                Name = culture.Lang.metName,
                Descript = culture.Lang.metDescript,
                Setting = JsonHelper.Serialize(setting)
            };

            //��˷��б�
            Microphones = RecordingHelper.Microphones();
            Microphone = Microphones.FirstOrDefault();

            //��¼����
            RecordFontSize = setting.FontSize;

            //¼����
            recorder = new RecorderWaveInService2();
        }
        private void SyncData()
        {
            //�������һ������,�ն�xx��� ����
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
                        last.Content = last.Content + "��";
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
                    record.Content = record.Content + "��";
                }
                if (record.Content.IsEmpty())
                {
                    record.Content = model.Content.TrimStartPunctuation();
                }
                else
                {
                    record.Content = record.Content + model.Content;
                }

                record.IsComplete = model.SyncType == 0; //-1:��ǰ����δ��,0����������Ϊ������
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