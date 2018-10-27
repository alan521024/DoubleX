namespace UTH.Meeting.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Diagnostics;
    using UTH.Infrastructure.Resource;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Speech;
    using UTH.Meeting.Domain;

    /// <summary>
    /// 远程服务对象
    /// </summary>
    public class ServerMarshalByRefObject : MarshalByRefObject, IServerMarshalByRefObject
    {
        /// <summary>
        /// 是否己连接
        /// </summary>
        public bool IsConnection { get; set; } = true;

        /// <summary>
        /// 会话信息
        /// </summary>
        IApplicationSession session;
        string culture, clientIp, appCode, token;

        /// <summary>
        /// 登录信息
        /// </summary>
        public void Sign(string tokenStr)
        {
            WpfHelper.SignIn(tokenStr);
            session = EngineHelper.Resolve<IApplicationSession>();
            culture = session.Accessor.Culture.Name;
            clientIp = session.Accessor.ClientIp;
            appCode = session.Accessor.AppCode;
            token = tokenStr;

            Console.WriteLine($"Service Sign status:{session.IsAuthenticated}, account: {session.User.Account}");
        }


        ISpeechService speechService = new iFlySpeechService();
        ILog speechLog = LoggingManager.GetLogger("Speech");

        public void SpeechStart(MeetingSettingModel setting)
        {
            speechService.Configruation((opt) =>
            {
                opt.AppId = "5a31dec7";
                opt.Rate = setting.Rate;
                opt.BitDepth = setting.BitDepth;
                opt.Channel = setting.Channel;
                opt.BufferMilliseconds = setting.BufferMilliseconds;
                opt.SentenceMilliseconds = setting.SentenceMilliseconds;
                opt.ByteLength = setting.ByteLength;
                opt.Speed = MeetingHelper.ConvertiFlySpeed(setting.Speed);

                opt.Language = MeetingHelper.GetiFlySpeechLang(setting.SourceLang);
                opt.Accent = MeetingHelper.ConvertiFlyAccent(setting.Accent);//"mandarin";

                opt.OnResult = (key, result, complete) =>
                {
                    MeetingDataAdd(key, new MeetingSyncModel()
                    {
                        MeetingId = key,
                        Content = result,
                        SyncType = complete ? 0 : -1,
                    });

                    var msg = $" Speech - OnResult : key[{key}] / result[{result}] / complete[{complete} / {DateTime.Now.ToString()} \n\r";
                    Console.WriteLine(msg);
                    speechLog.Info(msg);
                };
                opt.OnSentence = (key, sentence) =>
                {
                    speechLog.Info($" Speech - OnSentence : key[{key}] / sentence[{sentence}] / {DateTime.Now}");
                };
                opt.OnMessage = (key, msg) =>
                {
                    speechLog.Info($" Speech - OnMessage : key[{key}] / OnMessage[{msg}] / {DateTime.Now} \n\r");
                };
                opt.OnError = (key, error) =>
                {
                    //Trace.WriteLine
                    //Console.WriteLine 

                    speechLog.Info($" Speech - OnError : key[{key}] / error[{error}] / {DateTime.Now}");
                    EngineHelper.LoggingError(error);
                    speechService.Restart();
                };
            });
            speechService.Start();
        }

        public void SpeechStop()
        {
            speechService?.Stop();
        }

        public void SpeechSend(SpeechData data)
        {
            if (data == null)
                return;

            speechService?.Send(data);
        }


        Hashtable syncMeeting = new Hashtable();
        Thread remotingTask = null;
        DateTime lastRecordDt = DateTimeHelper.DefaultDateTime, lastTranslationDt = DateTimeHelper.DefaultDateTime;

        /// <summary>
        /// 会议同步任务
        /// </summary>
        public void MeetingSyncTask()
        {
            remotingTask?.Abort();
            remotingTask = null;

            remotingTask = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        foreach (Guid item in syncMeeting.Keys)
                        {
                            var input = new MeetingSyncInput() { MeetingId = item, RecordDt = lastRecordDt, TranslationDt = lastTranslationDt };
                            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingSyncQuery.GetResult<MeetingSyncOutput, MeetingSyncInput>(input,
                                    culture: culture, clientIp: clientIp, appCode: appCode, token: token);
                            if (result.Code == EnumCode.成功)
                            {
                                MeetingRemotingData(item, result.Obj);
                                MeetingLocalDBSave(item, result.Obj);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"MeetingSyncTask Error:{ExceptionHelper.GetMessage(ex)}");
                        EngineHelper.LoggingError(ex);
                    }
                    Thread.Sleep(1500);
                }
            });
            remotingTask.IsBackground = true;
            remotingTask.Start();
        }

        /// <summary>
        /// 获取会议记录(本地+线上)
        /// </summary>
        /// <returns></returns>
        public MeetingSyncModel MeetingDataGet(Guid key)
        {
            if (!syncMeeting.ContainsKey(key))
            {
                syncMeeting.Add(key, new ConcurrentQueue<MeetingSyncModel>());
            }

            var queue = syncMeeting[key] as ConcurrentQueue<MeetingSyncModel>;
            if (queue.IsNull())
                return null;

            MeetingSyncModel model = null;
            queue.TryDequeue(out model);
            return model;
        }

        /// <summary>
        /// 添加会议记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        public void MeetingDataAdd(Guid key, MeetingSyncModel model)
        {
            if (!syncMeeting.ContainsKey(key))
            {
                syncMeeting.Add(key, new ConcurrentQueue<MeetingSyncModel>());
            }

            var items = syncMeeting[key] as ConcurrentQueue<MeetingSyncModel>;
            if (items.IsNull())
                return;

            items.Enqueue(model);

            Console.WriteLine($"items：{items.Count}");
        }

        /// <summary>
        /// 添加远程会议/翻译记录
        /// </summary>
        /// <param name="model"></param>
        private void MeetingRemotingData(Guid meetingId, MeetingSyncOutput model)
        {
            if (model.IsNull())
                return;

            if (model.Records.IsEmpty() && model.Translations.IsEmpty())
                return;

            if (!model.Records.IsEmpty())
            {
                model.Records.OrderBy(x => x.LastDt).ToList().ForEach(x =>
                {
                    if (x.LastDt > lastRecordDt)
                    {
                        lastRecordDt = x.LastDt;
                    }
                    x.SyncType = 1;
                    if (x.LocalId.IsEmpty())
                    {
                        x.LocalId = Guid.NewGuid();
                    }
                    MeetingDataAdd(meetingId, x);
                });
            }

            if (!model.Translations.IsEmpty())
            {
                model.Translations.ForEach(x =>
                {
                    if (x.LastDt > lastTranslationDt)
                    {
                        lastTranslationDt = x.LastDt;
                    }
                    x.SyncType = 2;
                    MeetingDataAdd(meetingId, x);
                });
            }
        }

        /// <summary>
        /// 保存远程会议/翻译记录
        /// </summary>
        /// <param name="model"></param>
        private void MeetingLocalDBSave(Guid meetingId, MeetingSyncOutput model)
        {
            if (model.IsNull())
                return;

            if (model.Records.IsEmpty() && model.Translations.IsEmpty())
                return;

            var recordService = MeetingHelper.GetRecordService(meetingId);
            var translationService = MeetingHelper.GetTranslateService(meetingId);

            ThreadPool.QueueUserWorkItem((Object state) =>
            {
                if (!model.Records.IsEmpty())
                {
                    model.Records.ForEach(x =>
                    {
                        if (recordService.Find(where: i => i.Id == x.RecordId).Count() == 0)
                        {
                            recordService.Insert(EngineHelper.Map<MeetingRecordEntity>(x));
                        }
                    });
                }

                if (!model.Translations.IsEmpty())
                {
                    model.Translations.ForEach(x =>
                    {
                        if (translationService.Find(where: i => i.Id == x.TranslationId).Count() == 0)
                        {
                            translationService.Insert(EngineHelper.Map<MeetingTranslationEntity>(x));
                        }
                    });
                }
            });
        }
    }
}
