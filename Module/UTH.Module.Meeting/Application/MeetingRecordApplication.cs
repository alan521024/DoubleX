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
        ApplicationCrudService<IMeetingRecordDomainService, MeetingRecordEntity, MeetingRecordDTO, MeetingRecordEditInput>,
        IMeetingRecordApplication
    {
        IDomainDefaultService<MeetingTranslationEntity> translateService;

        public MeetingRecordApplication(IMeetingRecordDomainService _service, IDomainDefaultService<MeetingTranslationEntity> _translateService, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
            translateService = _translateService;
        }

        /// <summary>
        /// 创建会议记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回同步数所有(会议记录+翻译列表)</returns>
        public List<MeetingSyncModel> Create(MeetingSyncModel input)
        {
            List<MeetingSyncModel> results = new List<MeetingSyncModel>();

            var recEntity = MapperToEntity(input);
            if (service.Insert(recEntity).IsNull())
            {
                return results;
            }
            var recModel = EngineHelper.Map<MeetingSyncModel>(recEntity);

            if (!StringHelper.Punctuations.Contains(recModel.Content.Substring(recModel.Content.Length - 1, 1)))
            {
                recModel.Content = recModel.Content + (recModel.Langue == "zs" ? "。" : ".");
            }
            recModel.SyncType = 1;
            recModel.RefreshDt = DateTime.Now;
            results.Add(recModel);


            var translations = PlugCoreHelper.TranslationGet(input.Content, input.Langue, input.LangueTrs);
            if (translations.IsEmpty())
            {
                return results;
            }

            var trsEntitys = new List<MeetingTranslationEntity>();
            translations.ForEach(item =>
            {
                trsEntitys.Add(new MeetingTranslationEntity()
                {
                    MeetingId = recEntity.MeetingId,
                    RecordId = recEntity.Id,
                    Langue = item.TgtLang,
                    Content = item.TgtText,
                    Sort = 0
                });
            });
            if (!translateService.Insert(trsEntitys).IsEmpty())
            {
                var trsModels = EngineHelper.Map<List<MeetingSyncModel>>(trsEntitys);
                trsModels.ForEach(item =>
                {
                    item.SyncType = 2;
                    item.RefreshDt = DateTime.Now;
                });
                results.AddRange(trsModels);
            }

            return results;
        }

        /// <summary>
        /// 添加会议记录(含翻译记录)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual MeetingRecordOutput Add(MeetingRecordEditInput input)
        {
            if (!StringHelper.Punctuations.Contains(input.Content.Substring(input.Content.Length - 1, 1)))
            {
                input.Content = input.Content + (input.Langue == "zs" ? "。" : ".");
            }

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
            if (!result.IsNull())
            {
                output.Translations = EngineHelper.Map<List<MeetingTranslationDTO>>(inputs);
            }

            return output;
        }
    }
}
