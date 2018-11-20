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
    /// 个人用户领域服务接口
    /// </summary>
    public interface IMemberDomainService : IDomainDefaultService<MemberEntity>
    {
        /// <summary>
        /// 获取默认名称
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        string GetDefaultName(string inputName, AccountEntity account);
    }
}
