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
    /// 账号仓储
    /// </summary>
    public class AccountRepository : SqlSugarRepository<AccountEntity>, IAccountRepository
    {
        #region 构造方法
        public AccountRepository(ConnectionModel connectionModel) : base(connectionModel: connectionModel)
        {

        }
        #endregion
    }
}
