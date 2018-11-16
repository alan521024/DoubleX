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
    /// 账户仓储
    /// </summary>
    public class AccountRepository : SqlSugarRepository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(IUnitOfWorkManager unitMgr, ConnectionModel model = null, IApplicationSession session = null) :
            base(unitMgr, model, session)
        {

        }

        #region override

        protected override ISugarQueryable<AccountEntity> GetQueryable(ISugarQueryable<AccountEntity> query = null, Expression<Func<AccountEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = context.Queryable<AccountEntity, OrganizeEntity, EmployeEntity>((st, sc, sc2) => new object[] {
                JoinType.Left,st.Id==sc.Id,
                JoinType.Left, st.Id == sc2.Id})
                .Select((st, sc, sc2) => new AccountEntity() { Id = SqlFunc.GetSelfAndAutoFill(st.Id), OrganizeCode = sc.Code, EmployeCode = sc2.Code });

            source = source.MergeTable();

            return base.GetQueryable(source, where, sorting);
        }

        #endregion
    }
}
