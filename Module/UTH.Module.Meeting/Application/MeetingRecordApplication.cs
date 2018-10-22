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
    public class MeetingRecordApplication :
        ApplicationCrudService<IMeetingRecordDomainService,MeetingRecordEntity, MeetingRecordDTO, MeetingRecordEditInput>,
        IMeetingRecordApplication
    {
        IDomainDefaultService<MeetingTranslationEntity> translateService;

        public MeetingRecordApplication(IMeetingRecordDomainService _service, IDomainDefaultService<MeetingTranslationEntity> _translateService, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
            translateService = _translateService;
        }

        #region override

        #endregion

        /// <summary>
        /// 添加会议记录(含翻译记录)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual MeetingRecordOutput Add(MeetingRecordEditInput input)
        {
            var dto = base.Insert(input);
            var output = EngineHelper.Map<MeetingRecordOutput>(dto);
            if (output.IsNull())
                return output;

            output.Translations = new List<MeetingTranslationDTO>();

            //考虑替换多语言翻译接口
            var translations = PlugCoreHelper.TranslationGet(output.Content, output.Langue, output.LangueTrs);
            if (translations.IsEmpty())
            {
                return output;
            }

            var inputs = new List<MeetingTranslationEntity>();
            translations.ForEach(item =>
            {
                inputs.Add(new MeetingTranslationEntity()
                {
                    MeetingId = output.MeetingId,
                    RecordId = output.Id,
                    Langue = item.TgtLang,
                    Content = item.TgtText,
                    Sort = 0
                });
            });

            var result = translateService.InsertAsync(inputs).Result;
            if (result > 0)
            {
                output.Translations = EngineHelper.Map<List<MeetingTranslationDTO>>(inputs);
            }

            return output;
        }


    }
}
