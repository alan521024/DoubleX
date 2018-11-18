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
    /// 个人个人用户仓储
    /// </summary>
    public class MemberRepository : SqlSugarRepository<MemberEntity>, IMemberRepository, IRepository<MemberEntity>
    {
        public MemberRepository(IUnitOfWorkManager unitMgr, SqlSugarClient context = null,ConnectionModel model = null,  IApplicationSession session = null) :
            base(unitMgr, model, session)
        {

        }

        #region override

        protected override ISugarQueryable<MemberEntity> GetQueryable(ISugarQueryable<MemberEntity> query = null, Expression<Func<MemberEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            var source = context.Queryable<MemberEntity, AccountEntity>((st, sc) => new object[] {
                JoinType.Left,st.Id==sc.Id})
                .Select((st, sc) => new MemberEntity()
                {
                    Id = SqlFunc.GetSelfAndAutoFill(sc.Id),
                    Name = st.Name,
                    Gender = st.Gender,
                    Birthdate = st.Birthdate
                });

            source = source.MergeTable();

            return base.GetQueryable(source, where, sorting);
        }

        #endregion
    }
}
