namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 账号领域服务接口
    /// </summary>
    public interface IAccountDomain : IDomainDefaultService<AccountEntity>
    {
        /// <summary>
        /// 查找账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        AccountEntity GetAccount(string account = null, string mobile = null, string email = null, string userName = null);

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="organize"></param>
        /// <returns></returns>
        AccountEntity CreateAccount(AccountEntity account, OrganizeEntity organize = null);
    }
}
