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
    /// 应用版本仓储
    /// </summary>
    public class AppVersionRepository : SqlSugarRepository<AppVersionEntity>, IAppVersionRepository
    {
        public AppVersionRepository(SqlSugarClient context = null, ConnectionModel model = null, IApplicationSession session = null) :
            base(context, model, session)
        {

        }

        #region override

        protected override ISugarQueryable<AppVersionEntity> GetQueryable(ISugarQueryable<AppVersionEntity> query = null, Expression<Func<AppVersionEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = context.Queryable<AppVersionEntity, AppEntity>((st, sc) => new object[] {
               JoinType.Left,st.AppId==sc.Id})
                .Select((st, sc) => new AppVersionEntity()
                {
                    Id = SqlFunc.GetSelfAndAutoFill(st.Id),
                    AppName = sc.Name,
                    AppCode = sc.Code
                });
            source = source.MergeTable();
            return base.GetQueryable(source, where, sorting);
        }

        #endregion
    }
}
