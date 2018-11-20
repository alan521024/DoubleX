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
    /// 账户领域服务接口
    /// </summary>
    public interface IAccountDomainService : IDomainDefaultService<AccountEntity>
    {
        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        AccountEntity Get(string account = null, string mobile = null, string email = null, string userName = null, string no = null);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="organize"></param>
        /// <param name="employe"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        AccountEntity Create(Guid id, string account, string mobile, string email, string userName, string password, string organize, string employe, bool isAdmin);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        AccountEntity Create(AccountEntity account);

        /// <summary>
        /// 账户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="organize"></param>
        /// <returns></returns>
        AccountEntity Login(string account, string mobile, string email, string userName, string password, string organize);

    }
}
