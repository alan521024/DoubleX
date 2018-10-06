namespace UTH.Module.Meeting
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
    /// 会议账号配置信息业务
    /// </summary>
    public class MeetingProfileService : ApplicationDefault<MeetingProfileEntity, MeetingProfileEditInput, MeetingProfileOutput>, IMeetingProfileService
    {
        #region 构造函数

        public MeetingProfileService(IRepository<MeetingProfileEntity> _repository) : base(_repository)
        {
        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作


        #endregion

        #region 重写操作

        public override Action<MeetingProfileEditInput> InsertBeforeCall => base.InsertBeforeCall;
        public override Func<MeetingProfileOutput, MeetingProfileOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<MeetingProfileEditInput, MeetingProfileEntity, MeetingProfileEntity> UpdateBeforeCall => (input, entity) =>
        {
            entity.SourceLang = input.SourceLang;
            entity.TargetLangs = input.TargetLangs;
            entity.Speed = input.Speed;
            entity.FontSize = input.FontSize;
            entity.LastDt = DateTime.Now;
            return entity;
        };
        public override Func<MeetingProfileOutput, MeetingProfileOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<MeetingProfileEntity, bool>> FindWhere(QueryInput param)
        {
            var exp = ExpressHelper.Get<MeetingProfileEntity>();

            #region Input

            if (!param.IsNull() && !param.Query.IsNull())
            {
                string key = param.Query.GetString("key"), name = param.Query.GetString("name");
                int appType = param.Query.GetInt("appType");

                //exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                //exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                //exp = exp.AndIF(!appType.IsEmpty(), x => x.AppType == appType);
            }

            #endregion

            return exp.ToExpression();
        }

        #endregion

        /// <summary>
        /// 获取当前登录账号(Session)配置,如不存在，创建
        /// </summary>
        /// <returns></returns>
        public MeetingProfileOutput GetLoginAccountProfile()
        {
            Session.AccountId.CheckEmpty();

            var entity = repository.Find(x => x.AccountId == Session.AccountId);
            if (entity.IsNull())
            {
                entity = new MeetingProfileEntity()
                {
                    AccountId = Session.AccountId,
                    SourceLang = "zs",
                    TargetLangs = "en",
                    Speed = 5,
                    FontSize = 16
                };
                if (repository.Insert(entity) == 0)
                {
                    return null;
                }
            }
            return EngineHelper.Map<MeetingProfileOutput>(entity);
        }

        /// <summary>
        /// 保存当前登录账号(Session)配置
        /// </summary>
        /// <returns></returns>
        public MeetingProfileOutput SaveLoginAccountProfile(MeetingProfileEditInput input)
        {
            Session.AccountId.CheckEmpty();

            var entity = repository.Find(x => x.AccountId == Session.AccountId);
            entity.CheckNull();

            input.Id = entity.Id;

            var output = Update(input);

            if (!output.IsEmpty() && !input.MeetingId.IsEmpty())
            {
                var meetingRep = EngineHelper.Resolve<IMeetingRepository>();
                var meeting = meetingRep.Find(input.MeetingId);
                meeting.CheckNull();
                meeting.Setting = JsonHelper.Serialize(EngineHelper.Map<MeetingSettingModel>(output));
                meetingRep.Update(meeting);
            }

            return output;
        }
    }
}
