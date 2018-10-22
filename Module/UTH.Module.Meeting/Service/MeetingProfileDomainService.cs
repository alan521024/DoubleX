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
    ///会议配置领域服务
    /// </summary>
    public class MeetingProfileDomainService : DomainDefaultService<MeetingProfileEntity>, IMeetingProfileDomainService
    {
        public MeetingProfileDomainService(IRepository<MeetingProfileEntity> repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
        }

        #region override

        protected override void UpdateBefore(List<MeetingProfileEntity> list)
        {
            if (list.IsEmpty())
                return;

            var ids = list.Select(x => x.Id).ToList();
            var entitys = Find(where: x => ids.Contains(x.Id));

            foreach (var entity in entitys)
            {
                var input = list.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (input.IsNull())
                {
                    continue;
                }
                entity.SourceLang = input.SourceLang;
                entity.TargetLangs = input.TargetLangs;
                entity.Speed = input.Speed;
                entity.FontSize = input.FontSize;
                entity.LastDt = DateTime.Now;
            }

            list = entitys;

            base.UpdateBefore(list);
        }

        #endregion
    }
}
