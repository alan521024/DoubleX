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
    /// 应用程序仓储
    /// </summary>
    public class AppRepository : SqlSugarRepository<AppEntity>, IAppRepository
    {
        #region 构造方法

        public AppRepository(string connectionStr = null, ConnectionModel connectionModel = null, SqlSugarClient connectionClient = null, IApplicationSession session = null) :
            base(connectionStr, connectionModel, connectionClient, session)
        {

        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作

        #endregion

        public override List<AppEntity> Paging(int page, int size, Expression<Func<AppEntity, bool>> predicate, List<KeyValueModel> sorting, ref int total)
        {
            //var getAll = client.Queryable<AppVersionEntity, AppEntity>((st, sc) => new object[] {
            //            JoinType.Left,st.AppId==sc.Id})
            //            .Where(st => SqlFunc.Subqueryable<AppEntity>().Where(s => s.Id == st.AppId).Any())
            //            .ToList();

            //var subQuery = SqlFunc.Subqueryable<AppEntity>().Where(predicate);
            
            //var query = client.Queryable<AppVersionEntity, AppEntity>((st, sc) => new object[] {
            //                JoinType.Left,st.AppId==sc.Id})
            //                .Where(st => SqlFunc.Subqueryable<AppEntity>().Where(predicate.Body).Any());

            //int count = 0;
            //var ss = query.ToPageList(page, size, ref count);

            return base.Paging(page, size, predicate, sorting, ref total);
        }
    }
}
