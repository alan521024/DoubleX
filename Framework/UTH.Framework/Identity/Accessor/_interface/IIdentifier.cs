namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Principal;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// UTH 身份信息对象 扩展接口
    /// </summary>
    public interface IIdentifier : IIdentity
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        string AccountId { get; }

        /// <summary>
        /// 账号
        /// </summary>
        string Account { get; set; }
    }
}
