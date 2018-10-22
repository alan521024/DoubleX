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
    /// 人员领域服务
    /// </summary>
    public class MeetingRecordDomainService : DomainDefaultService<MeetingRecordEntity>, IMeetingRecordDomainService
    {
        public MeetingRecordDomainService(IRepository<MeetingRecordEntity> repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
        }

        #region override

        protected override void InsertBefore(List<MeetingRecordEntity> list)
        {
            if (list.IsEmpty())
                return;
            list.ForEach(item =>
            {
                if (item.LocalId.IsEmpty())
                {
                    item.LocalId = Guid.NewGuid();
                }
            });

            base.InsertBefore(list);
        }

        #endregion
    }
}
