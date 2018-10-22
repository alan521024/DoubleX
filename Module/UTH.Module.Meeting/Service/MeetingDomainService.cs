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
    public class MeetingDomainService : DomainDefaultService<IMeetingRepository, MeetingEntity>, IMeetingDomainService
    {
        public MeetingDomainService(IMeetingRepository repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
        }

        #region override

        protected override void InsertBefore(List<MeetingEntity> list)
        {
            var queryNum = Max<string>(field: x => x.Num);
            if (queryNum.IsEmpty())
            {
                queryNum = "100000";
            }
            var currentNum = IntHelper.Get(queryNum);
            list.ForEach(item =>
            {
                currentNum = currentNum + 1;
                item.Num = StringHelper.Get(currentNum);
            });
            base.InsertBefore(list);
        }

        #endregion

        /// <summary>
        /// 查找同步记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MeetingSyncOutput FindSyncQuery(MeetingSyncInput input)
        {
            return Repository.FindSyncQuery(input);
        }
    }
}
