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
    /// 会议记录信息业务
    /// </summary>
    public class MeetingRecordService : ApplicationDefault<MeetingRecordEntity, MeetingRecordEditInput, MeetingRecordOutput>, IMeetingRecordService
    {
        #region 构造函数

        public MeetingRecordService(IRepository<MeetingRecordEntity> _repository) : base(_repository)
        {
            translationRep = EngineHelper.Resolve<IRepository<MeetingTranslationEntity>>();
        }

        #endregion

        #region 私有变量

        private IRepository<MeetingTranslationEntity> translationRep;

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作

        #endregion

        #region 重写操作

        public override Action<MeetingRecordEditInput> InsertBeforeCall => (input) =>
        {
            if (input.LocalId.IsEmpty())
            {
                input.LocalId = Guid.NewGuid();
            }
        };

        public override Func<MeetingRecordOutput, MeetingRecordOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<MeetingRecordEditInput, MeetingRecordEntity, MeetingRecordEntity> UpdateBeforeCall => base.UpdateBeforeCall;
        public override Func<MeetingRecordOutput, MeetingRecordOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<MeetingRecordEntity, bool>> FindPredicate(QueryInput param)
        {
            var exp = ExpressHelper.Get<MeetingRecordEntity>();

            #region Input

            if (!param.IsNull() && !param.Query.IsNull())
            {
                string key = param.Query.GetString("key");
                Guid meetingId = param.Query.GetGuid("meetingid");

                //exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
            }

            #endregion

            return exp.ToExpression();
        }

        #endregion

        /// <summary>
        /// 添加会议记录(含翻译记录)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual MeetingRecordOutput Add(MeetingRecordEditInput input)
        {
            var output = base.Insert(input);
            if (output.IsNull())
                return output;

            output.Translations = new List<MeetingTranslationOutput>();

            var trsRep = EngineHelper.Resolve<IRepository<MeetingTranslationEntity>>(new KeyValueModel<string, object>("connectionModel", repository.Connection));
            var trsService = EngineHelper.Resolve<IMeetingTranslationService>(new KeyValueModel<string, object>("_repository", trsRep));

            //考虑替换多语言翻译接口
            var translations = PlugCoreHelper.TranslationGet(output.Content, output.Langue, output.LangueTrs);
            if (translations.IsEmpty())
            {
                return output;
            }

            var inputs = new List<MeetingTranslationEditInput>();
            translations.ForEach(item =>
            {
                inputs.Add(new MeetingTranslationEditInput()
                {
                    MeetingId = output.MeetingId,
                    RecordId = output.Id,
                    Langue = item.TgtLang,
                    Content = item.TgtText,
                    Sort = 0
                });
            });
            output.Translations = trsService.InsertBatchAsync(inputs).Result;

            return output;
        }


    }
}
