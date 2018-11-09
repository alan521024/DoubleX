namespace UTH.Module.Basics
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

    /// <summary>
    /// 应用版本领域服务
    /// </summary>
    public class AppVersionDomainService : DomainDefaultService<IAppVersionRepository, AppVersionEntity>, IAppVersionDomainService
    {
        public AppVersionDomainService(IAppVersionRepository repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
        }

        #region override
        
        protected override List<AppVersionEntity> UpdateBefore(List<AppVersionEntity> inputs)
        {
            if (inputs.IsEmpty())
                return inputs;

            var ids = inputs.Select(x => x.Id).ToList();
            var entitys = Find(where: x => ids.Contains(x.Id));

            foreach (var entity in entitys)
            {
                var input = inputs.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (input.IsNull())
                {
                    continue;
                }
                entity.AppId = input.AppId;
                entity.No = input.No;
                entity.Descript = input.Descript;
                entity.UpdateType = input.UpdateType;
                entity.ReleaseDt = input.ReleaseDt;
                entity.FileMd5 = input.FileMd5;
                entity.FileName = input.FileName;
            }

            return entitys;
        }

        #endregion
    }
}
