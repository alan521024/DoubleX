namespace UTH.Module.User
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
    /// 人员仓储
    /// </summary>
    public class EmployeRepository : SqlSugarRepository<EmployeEntity>, IEmployeRepository, IRepository<EmployeEntity>
    {
        public EmployeRepository(SqlSugarClient context = null, ConnectionModel model = null, IApplicationSession session = null) :
            base(context, model, session)
        {

        }

        protected override ISugarQueryable<EmployeEntity> GetQueryable(ISugarQueryable<EmployeEntity> query = null, Expression<Func<EmployeEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = context.Queryable<EmployeEntity, AccountEntity>((st, sc) => new object[] {
                JoinType.Left,st.Id==sc.Id})
                .Select((st, sc) => new EmployeEntity()
                {
                    Id = SqlFunc.GetSelfAndAutoFill(st.Id),
                    Status = sc.Status
                });

            source = source.MergeTable();

            return base.GetQueryable(source, where, sorting);
        }
    }
}
