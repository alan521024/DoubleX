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
    using UTH.Plug;

    /// <summary>
    /// 应用设置仓储
    /// </summary>
    public class AppSettingRepository : SqlSugarRepository<AppSettingEntity>, IAppSettingRepository
    {
        public AppSettingRepository(ConnectionModel model = null, SqlSugarClient client = null, IApplicationSession session = null) :
            base(model, client, session)
        {

        }

        #region override

        protected override ISugarQueryable<AppSettingEntity> GetQueryable(ISugarQueryable<AppSettingEntity> query = null, Expression<Func<AppSettingEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = client.Queryable<AppSettingEntity, AppEntity>((st, sc) => new object[] {
               JoinType.Left,st.AppId==sc.Id})
                .Select((st, sc) => new AppSettingEntity()
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
