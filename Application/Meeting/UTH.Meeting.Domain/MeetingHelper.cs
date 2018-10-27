namespace UTH.Meeting.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 应用辅助操作
    /// </summary>
    public static class MeetingHelper
    {
        #region 语言/音速转换接口对应值

        /// <summary>
        /// 获取语音(iFly识别服务使用)
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetiFlySpeechLang(string lang, string split = "|")
        {
            if (!lang.IsEmpty())
            {
                var arr = StringHelper.GetToArray(lang, new string[] { split });
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] = ConvertiFlySpeechLang(arr[i]);
                }
                return string.Join("|", arr);
            }
            return lang;
        }

        /// <summary>
        /// 转换语言(iFly识别服务使用)
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string ConvertiFlySpeechLang(string lang)
        {
            switch (lang.ToLower())
            {
                case "zs":
                    return "zh_cn";
                case "en":
                    return "en_us";
                default:
                    return lang;
            }
        }

        /// <summary>
        /// 转换方言(iFly识别服务使用)
        /// </summary>
        /// <param name="accent"></param>
        /// <returns></returns>
        public static string ConvertiFlyAccent(string accent)
        {
            //mandarin
            return accent;
        }

        /// <summary>
        /// 录音服务()
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static int ConvertiFlySpeed(int speed)
        {
            //高：10  中:5  低:1
            if (speed == 10)
            {
                return 900;
            }
            if (speed == 5)
            {
                return 780;
            }
            if (speed == 1)
            {
                return 400;
            }
            return 780;
        }

        #endregion

        #region 本地数据库业务

        public static string TemplateDatabaseFilePath
        {
            get
            {
                return FilesHelper.GetPath("Assets/Template/database.db", isAppWork: true);
            }
        }

        public static string GetMeetingDatabaseFile(Guid meetingId)
        {
            return FilesHelper.GetPath(string.Format("Assets/Data/{0}/database.db", meetingId.IsEmpty() ? "Temp" : meetingId.ToString()), isAppWork: true);
        }

        public static string GetMeetingWavFile(Guid meetingId)
        {
            return FilesHelper.GetPath(string.Format("Assets/Data/{0}/{1}.wav", meetingId.IsEmpty() ? "Temp" : meetingId.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")), isAppWork: true);
        }

        public static string GetMeetingTextFile(Guid meetingId)
        {
            return FilesHelper.GetPath(string.Format("Assets/Data/{0}/{1}.txt", meetingId.IsEmpty() ? "Temp" : meetingId.ToString(), "会议记录"), isAppWork: true);
        }

        public static IDomainDefaultService<MeetingRecordEntity> GetRecordService(Guid meetingId)
        {
            var par = new List<KeyValueModel<string, object>>();
            par.Add(new KeyValueModel<string, object>("session", EngineHelper.Resolve<IApplicationSession>()));
            par.Add(new KeyValueModel<string, object>("caching", EngineHelper.Resolve<ICachingService>(new KeyValueModel<string, object>("model", EngineHelper.Configuration.Store.Caching))));


            var repParams = new List<KeyValueModel<string, object>>();
            repParams.AddRange(par);
            repParams.Add(new KeyValueModel<string, object>("connectionModel", new ConnectionModel()
            {
                DbType = EnumDbType.Sqlite,
                ConnectionString = string.Format("Data Source={0};", MeetingHelper.GetMeetingDatabaseFile(meetingId))
            }));
            var repository = EngineHelper.Resolve<IRepository<MeetingRecordEntity>>(repParams.ToArray());


            var serviceParams = new List<KeyValueModel<string, object>>();
            serviceParams.AddRange(par);
            serviceParams.Add(new KeyValueModel<string, object>("_repository", repository));
            var service = EngineHelper.Resolve<IDomainDefaultService<MeetingRecordEntity>>(serviceParams.ToArray());

            return service;
        }

        public static IDomainDefaultService<MeetingTranslationEntity> GetTranslateService(Guid meetingId)
        {
            var par = new List<KeyValueModel<string, object>>();
            par.Add(new KeyValueModel<string, object>("session", EngineHelper.Resolve<IApplicationSession>()));
            par.Add(new KeyValueModel<string, object>("caching", EngineHelper.Resolve<ICachingService>(new KeyValueModel<string, object>("model", EngineHelper.Configuration.Store.Caching))));


            var repParams = new List<KeyValueModel<string, object>>();
            repParams.AddRange(par);
            repParams.Add(new KeyValueModel<string, object>("connectionModel", new ConnectionModel()
            {
                DbType = EnumDbType.Sqlite,
                ConnectionString = string.Format("Data Source={0};", MeetingHelper.GetMeetingDatabaseFile(meetingId))
            }));
            var repository = EngineHelper.Resolve<IRepository<MeetingTranslationEntity>>(repParams.ToArray());


            var serviceParams = new List<KeyValueModel<string, object>>();
            serviceParams.AddRange(par);
            serviceParams.Add(new KeyValueModel<string, object>("_repository", repository));
            var service = EngineHelper.Resolve<IDomainDefaultService<MeetingTranslationEntity>>(serviceParams.ToArray());

            return service;
        }

        #endregion

        #region 获取会议信息

        public static MeetingDTO GetMeeting(Guid? id = null, string code = null)
        {
            if (!id.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetId.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput()
                {
                    Id = id.Value
                });
                if (result.Code == EnumCode.成功)
                {
                    return result.Obj;
                }
            }
            else if (!code.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetCode.GetResult<MeetingDTO, MeetingEditInput>(new MeetingEditInput() { Num = code });
                if (result.Code == EnumCode.成功)
                {
                    return result.Obj;
                }
            }
            return null;
        }

        #endregion
    }
}