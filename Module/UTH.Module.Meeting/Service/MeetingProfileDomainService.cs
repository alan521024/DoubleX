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

        protected override List<MeetingProfileEntity> UpdateBefore(List<MeetingProfileEntity> list)
        {
            if (list.IsEmpty())
                return list;

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

            return entitys;
        }

        #endregion

        /// <summary>
        /// 获取账号会议配置(不存在时创建默认)
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public virtual MeetingProfileEntity GetOrInsertDefaultByAccount(Guid accountId)
        {
            accountId.CheckEmpty();
            var entity = Get(x => x.AccountId == Session.User.Id);
            if (entity.IsNull())
            {
                entity = Insert(new MeetingProfileEntity()
                {
                    AccountId = accountId,
                    SourceLang = "zs",
                    TargetLangs = "en",
                    Speed = 5,
                    FontSize = 16
                });
            }
            return entity;
        }
    }
}
