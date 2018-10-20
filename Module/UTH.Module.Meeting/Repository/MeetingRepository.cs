namespace UTH.Module.Meeting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using SqlSugar;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 会议仓储
    /// </summary>
    public class MeetingRepository : SqlSugarRepository<MeetingEntity>, IMeetingRepository
    {
        public MeetingRepository(ConnectionModel model = null, SqlSugarClient client = null, IApplicationSession session = null) :
            base(model, client, session)
        {

        }

        /// <summary>
        /// 查找同步记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MeetingSyncOutput FindSyncQuery(MeetingSyncInput input)
        {
            var recordRep = EngineHelper.Resolve<IRepository<MeetingRecordEntity>>(new KeyValueModel<string, object>("connectionClient", client));
            var translationRep = EngineHelper.Resolve<IRepository<MeetingTranslationEntity>>(new KeyValueModel<string, object>("connectionClient", client));

            MeetingSyncOutput output = new MeetingSyncOutput();
            output.Records = new List<MeetingSyncModel>();
            output.Translations = new List<MeetingSyncModel>();

            var recordExp = ExpressHelper.Get<MeetingRecordEntity>();
            recordExp = recordExp.And(x => x.MeetingId == input.MeetingId);
            if (input.Direction == EnumDirection.Next)
            {
                recordExp = recordExp.And(x => x.LastDt > input.RecordDt);
            }
            else
            {
                recordExp = recordExp.And(x => x.LastDt < input.RecordDt);
            }

            var lastTimespan = DateTimeHelper.GetToUnixTimestamp();
            var sorting = new List<KeyValueModel>() { new KeyValueModel("LastDt", "desc") };

            var records = recordRep.Find(input.Top, recordExp.ToExpression(), sorting);
            if (!records.IsEmpty())
            {
                output.Records = EngineHelper.Map<List<MeetingSyncModel>>(records);
                output.Records.ForEach(x => { x.SyncType = 1; });
            }

            var trsExp = ExpressHelper.Get<MeetingTranslationEntity>();
            trsExp = trsExp.And(x => x.MeetingId == input.MeetingId);
            if (input.Direction == EnumDirection.Next)
            {
                trsExp = trsExp.And(x => x.LastDt > input.TranslationDt);
            }
            else
            {
                trsExp = trsExp.And(x => x.LastDt < input.TranslationDt);
            }

            var translations = translationRep.Find(input.Top, trsExp.ToExpression(), sorting);
            if (!translations.IsEmpty())
            {
                output.Translations = EngineHelper.Map<List<MeetingSyncModel>>(translations);
                output.Translations.ForEach(x => { x.SyncType = 2; });
            }
            return output;
        }
    }
}
