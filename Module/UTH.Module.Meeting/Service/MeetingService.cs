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
    /// 会议信息业务
    /// </summary>
    public class MeetingService : ApplicationDefault<MeetingEntity, MeetingEditInput, MeetingOutput>, IMeetingService
    {
        #region 构造函数

        public MeetingService(IMeetingRepository _repository) : base(_repository)
        {
            meetingRep = _repository;
        }

        #endregion

        #region 私有变量

        private IMeetingRepository meetingRep;

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作

        #endregion

        #region 重写操作

        public override Action<MeetingEditInput> InsertBeforeCall => (input) =>
        {
            var maxNum = repository.Max<string>(field: x => x.Num);
            if (maxNum.IsEmpty())
            {
                input.Num = "100000";
            }
            else
            {
                input.Num = StringHelper.Get(IntHelper.Get(maxNum) + 1);
            }
        };
        public override Func<MeetingOutput, MeetingOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<MeetingEditInput, MeetingEntity, MeetingEntity> UpdateBeforeCall => (input, entity) =>
        {
            entity.Name = input.Name;
            //entity.AppType = input.AppType;
            return entity;
        };
        public override Func<MeetingOutput, MeetingOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<MeetingEntity, bool>> FindPredicate(QueryInput param)
        {
            var exp = ExpressHelper.Get<MeetingEntity>();

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
        /// 根据Code获取会议信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MeetingOutput GetByCode(MeetingEditInput input)
        {
            return Find(predicate: x => x.Num == input.Num).FirstOrDefault();
        }

        /// <summary>
        /// 获取同步数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual MeetingSyncOutput SyncQuery(MeetingSyncInput input)
        {
            return meetingRep.FindSyncQuery(input);
        }
    }
}
