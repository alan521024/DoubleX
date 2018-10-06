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
    /// 会议翻译信息业务
    /// </summary>
    public class MeetingTranslationService : ApplicationDefault<MeetingTranslationEntity, MeetingTranslationEditInput, MeetingTranslationOutput>, IMeetingTranslationService
    {
        #region 构造函数

        public MeetingTranslationService(IRepository<MeetingTranslationEntity> _repository) : base(_repository)
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

        public override Action<MeetingTranslationEditInput> InsertBeforeCall => base.InsertBeforeCall;
        public override Func<MeetingTranslationOutput, MeetingTranslationOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<MeetingTranslationEditInput, MeetingTranslationEntity, MeetingTranslationEntity> UpdateBeforeCall => base.UpdateBeforeCall;
        public override Func<MeetingTranslationOutput, MeetingTranslationOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<MeetingTranslationEntity, bool>> FindWhere(QueryInput param)
        {
            var exp = ExpressHelper.Get<MeetingTranslationEntity>();

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
    }
}
