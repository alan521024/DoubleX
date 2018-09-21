namespace UTH.Meeting.Server
{
    using System;
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
    /// 服务远程对象
    /// </summary>
    public class ServerMarshalByRefObject : MarshalByRefObject, IServerMarshalByRefObject
    {
        #region 公共属性

        /// <summary>
        /// 是否己连接
        /// </summary>
        public bool IsConnection { get; set; } = true;

        #endregion

        #region 私有变量

        //服务配置
        ServerOption options = new ServerOption();

        //会话信息
        IApplicationSession session;

        //识别服务
        ISpeechService speech;

        //会议信息, 会议同步队列，最后同步时间,业务处理
        MeetingBase meeting;
        ConcurrentQueue<MeetingSyncModel> meetingSyncQueue = new ConcurrentQueue<MeetingSyncModel>();
        DateTime LastRecordDt = DateTimeHelper.DefaultDateTime, LastTranslationDt = DateTimeHelper.DefaultDateTime;
        IMeetingRecordService recordService;
        IMeetingTranslationService translationService;

        #endregion

        #region 辅助操作

        private void MeetingCheck()
        {
            meeting.CheckNull();
            session.CheckNull();

            meeting.Id.CheckEmpty();
        }

        private void MeetingRemotingSync()
        {
            var input = new MeetingSyncInput() { MeetingId = meeting.Id, RecordDt = LastRecordDt, TranslationDt = LastTranslationDt };
            var result = PlugCoreHelper.ApiUrl.Meeting.MeetingSyncQuery.GetResult<MeetingSyncOutput, MeetingSyncInput>(session, input);
            if (result.Code == EnumCode.成功)
            {
                MeetingRemotingData(result.Obj);
                MeetingLocalDBSave(result.Obj);
            }
        }

        private void MeetingRemotingData(MeetingSyncOutput model)
        {
            if (model.IsNull())
                return;

            if (model.Records.IsEmpty() && model.Translations.IsEmpty())
                return;

            if (!model.Records.IsEmpty())
            {
                model.Records.OrderBy(x => x.LastDt).ToList().ForEach(x =>
                  {
                      if (x.LastDt > LastRecordDt)
                      {
                          LastRecordDt = x.LastDt;
                      }
                      x.SyncType = 1;
                      if (x.LocalId.IsEmpty())
                      {
                          x.LocalId = Guid.NewGuid();
                      }
                      meetingSyncQueue.Enqueue(x);
                  });
            }

            if (!model.Translations.IsEmpty())
            {
                model.Translations.ForEach(x =>
                {
                    if (x.LastDt > LastTranslationDt)
                    {
                        LastTranslationDt = x.LastDt;
                    }
                    x.SyncType = 2;
                    meetingSyncQueue.Enqueue(x);
                });
            }
        }

        private void MeetingLocalDBSave(MeetingSyncOutput model)
        {
            if (model.IsNull())
                return;

            if (model.Records.IsEmpty() && model.Translations.IsEmpty())
                return;

            ThreadPool.QueueUserWorkItem((Object state) =>
            {
                if (!model.Records.IsEmpty())
                {
                    model.Records.ForEach(x =>
                    {
                        if (recordService.Find(0, i => i.Id == x.RecordId).Count() == 0)
                        {
                            recordService.Insert(EngineHelper.Map<MeetingRecordEditInput>(x));
                        }
                    });
                }

                if (!model.Translations.IsEmpty())
                {
                    model.Translations.ForEach(x =>
                    {
                        if (translationService.Find(0, i => i.Id == x.TranslationId).Count() == 0)
                        {
                            translationService.Insert(EngineHelper.Map<MeetingTranslationEditInput>(x));
                        }
                    });
                }
            });
        }

        #endregion

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="setting"></param>
        public void Configuration(MeetingSettingModel setting)
        {
            options.MeetingSetting = setting;
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="action"></param>
        public void Configuration(Action<ServerOption> action)
        {
            action?.Invoke(options);
        }
        
        /// <summary>
        /// 设置会议信息
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="session"></param>
        public void MeetingInit(MeetingBase _meeting, IApplicationSession _session)
        {
            meeting = _meeting;
            session = _session;

            this.MeetingCheck();

            meetingSyncQueue = new ConcurrentQueue<MeetingSyncModel>();
            LastRecordDt = DateTimeHelper.DefaultDateTime;
            LastTranslationDt = DateTimeHelper.DefaultDateTime;

            var recordRepository = EngineHelper.Resolve<IRepository<MeetingRecordEntity>>(new KeyValueModel<string, object>("connectionModel", new ConnectionModel()
            {
                DbType = EnumDbType.Sqlite,
                ConnectionString = string.Format("Data Source={0};", MeetingHelper.GetMeetingDatabaseFile(meeting.IsNull() ? Guid.Empty : meeting.Id))
            }));
            recordService = EngineHelper.Resolve<IMeetingRecordService>(new KeyValueModel<string, object>("_repository", recordRepository));
            recordService.SetSession(session);

            var translationRepository = EngineHelper.Resolve<IRepository<MeetingTranslationEntity>>(new KeyValueModel<string, object>("connectionModel", new ConnectionModel()
            {
                DbType = EnumDbType.Sqlite,
                ConnectionString = string.Format("Data Source={0};", MeetingHelper.GetMeetingDatabaseFile(meeting.IsNull() ? Guid.Empty : meeting.Id))
            }));
            translationService = EngineHelper.Resolve<IMeetingTranslationService>(new KeyValueModel<string, object>("_repository", translationRepository));
            translationService.SetSession(session);

            var remotingTask = new Thread(() =>
           {
               try
               {
                   while (true)
                   {
                       MeetingRemotingSync();
                       Thread.Sleep(1500);
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           });
            remotingTask.IsBackground = true;
            remotingTask.Start();
        }

        /// <summary>
        /// 语音识别开始
        /// </summary>
        public void MeetingStart()
        {
            this.MeetingCheck();
            speech = new iFlySpeechService();
            speech.Configruation((opt) =>
            {
                opt.AppId = "5a31dec7";
                opt.Rate = options.MeetingSetting.Rate;
                opt.BitDepth = options.MeetingSetting.BitDepth;
                opt.Channel = options.MeetingSetting.Channel;
                opt.BufferMilliseconds = options.MeetingSetting.BufferMilliseconds;
                opt.ByteLength = options.MeetingSetting.ByteLength;

                opt.Speed = MeetingHelper.ConvertiFlySpeed(options.MeetingSetting.Speed);

                opt.Language = MeetingHelper.GetiFlySpeechLang(options.MeetingSetting.SourceLang);
                opt.Accent = MeetingHelper.ConvertiFlyAccent(options.MeetingSetting.Accent);          //"mandarin";

                opt.OnResult = (key, result, complete) =>
                {
                    Trace.WriteLine($" Speech - OnResult : key[{key}] / result[{result}] / {DateTime.Now}");
                    var model = new MeetingSyncModel();
                    model.MeetingId = key;
                    model.Content = result;
                    model.SyncType = complete ? 0 : -1;
                    meetingSyncQueue.Enqueue(model);
                };
                opt.OnSentence = (key, sentence) =>
                {
                    //Trace.WriteLine($" Speech - OnSentence : key[{key}] / sentence[{sentence}] / {DateTime.Now}");
                };
                opt.OnMessage = (key, msg) =>
                {
                    //Trace.WriteLine($" Speech - OnMessage : key[{key}] / msg[{msg}] / {DateTime.Now}");
                    //Console.WriteLine(string.Format("时间：{0} Info：{1} \n\r", DateTime.Now.ToString(), msg));
                };
                opt.OnError = (key, error) =>
                {
                    //Trace.WriteLine($" Speech - OnError : key[{key}] / error[{error}] / {DateTime.Now}");
                };
            });
            speech.Start();
        }

        /// <summary>
        /// 语音识别结束
        /// </summary>
        public void MeetingStop()
        {
            this.MeetingCheck();

            speech.Stop();
            speech = null;
        }

        /// <summary>
        /// 语音识别数据
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void MeetingSend(Guid meetingId, byte[] data, int offset, int length)
        {
            this.MeetingCheck();

            speech.Send(new SpeechData()
            {
                Key = meetingId,
                Data = data,
                Offset = offset,
                Length = length,
                LastDt = DateTime.Now
            });
        }

        /// <summary>
        /// 会议语音记录
        /// </summary>
        /// <param name="localId"></param>
        /// <param name="meetingId"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="content"></param>
        public void MeetingInsert(Guid localId, Guid meetingId, string source, string target, string content)
        {
            if (localId.IsEmpty() || meetingId.IsEmpty() || source.IsEmpty() || target.IsEmpty() || content.IsEmpty())
                return;

            ThreadPool.QueueUserWorkItem((Object state) =>
            {
                var input = new MeetingRecordEditInput()
                {
                    MeetingId = meetingId,
                    Langue = source,
                    LangueTrs = target,
                    Content = content,
                    LocalId = localId,
                    Sort = 0
                };
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingRecordAdd.GetResult<MeetingRecordOutput, MeetingRecordEditInput>(session, input);
                if (result.Code == EnumCode.成功)
                {
                    //....
                }
            });
        }

        /// <summary>
        /// 会议同步记录(本地+线上)
        /// </summary>
        /// <returns></returns>
        public MeetingSyncModel MeetingSync()
        {
            this.MeetingCheck();
            MeetingSyncModel model = null;
            meetingSyncQueue.TryDequeue(out model);
            return model;
        }
    }
}
