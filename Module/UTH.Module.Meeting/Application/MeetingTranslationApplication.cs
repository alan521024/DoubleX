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
    public class MeetingTranslationApplication :
        ApplicationCrudService<MeetingTranslationEntity, MeetingTranslationDTO, MeetingTranslationEditInput>,
        IMeetingTranslationApplication
    {
        public MeetingTranslationApplication(IDomainDefaultService<MeetingTranslationEntity> _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {

        }
    }
}
