namespace UTH.Module.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using SqlSugar;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 应用程序仓储
    /// </summary>
    public class AppRepository : SqlSugarRepository<AppEntity>, IAppRepository
    {
        public AppRepository(IUnitOfWorkManager unitMgr, ConnectionModel model = null, IApplicationSession session = null) :
            base(unitMgr, model, session)
        {

        }

        #region override

        public override AppEntity Get(Guid key)
        {
            return base.Get(key);
        }

        public override AppEntity Get(Expression<Func<AppEntity, bool>> predicate)
        {
            return base.Get(predicate);
        }

        public override List<AppEntity> Find(int top = 0, Expression<Func<AppEntity, bool>> predicate = null, List<KeyValueModel> sorting = null)
        {
            var list = base.Find(top, predicate, sorting);

            this.SetVersions(list);

            return list;
        }

        public override List<AppEntity> Paging(int page, int size, Expression<Func<AppEntity, bool>> predicate, List<KeyValueModel> sorting, ref int total)
        {
            var list = base.Paging(page, size, predicate, sorting, ref total);

            this.SetVersions(list);

            return list;
        }

        #endregion

        /// <summary>
        /// 设置应用集合的版本列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected List<AppEntity> SetVersions(List<AppEntity> list)
        {
            if (list.IsEmpty())
                return list;

            var ids = list.Select(x => x.Id).ToList();

            var versions = context.Queryable<AppVersionEntity>().Where(x => ids.Contains(x.AppId)).ToList();

            list.ForEach(item =>
            {
                item.Versions = versions.Where(x => x.AppId == item.Id).ToList();
            });

            return list;
        }


    }
}
