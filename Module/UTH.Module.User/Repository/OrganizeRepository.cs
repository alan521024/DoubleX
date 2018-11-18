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
    /// 组织用户仓储
    /// </summary>
    public class OrganizeRepository : SqlSugarRepository<OrganizeEntity>, IOrganizeRepository, IRepository<OrganizeEntity>
    {
        public OrganizeRepository(IUnitOfWorkManager unitMgr, ConnectionModel model = null, IApplicationSession session = null) :
            base(unitMgr, model, session)
        {

        }

        #region override

        protected override ISugarQueryable<OrganizeEntity> GetQueryable(ISugarQueryable<OrganizeEntity> query = null, Expression<Func<OrganizeEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = context.Queryable<OrganizeEntity, AccountEntity>((st, sc) => new object[] {
                JoinType.Left,st.Id==sc.Id})
                .Select((st, sc) => new OrganizeEntity()
                {
                    Id = SqlFunc.GetSelfAndAutoFill(sc.Id),
                    Code = st.Code,
                    Name = st.Name,
                    Phone = st.Phone,
                    Fax = st.Fax,
                    AreaCode = st.AreaCode,
                    Address = st.Address
                });

            source = source.MergeTable();

            return base.GetQueryable(source, where, sorting);
        }

        #endregion
    }
}
