namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Principal;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// UTH 身份信息对象 扩展
    /// </summary>
    [Serializable]
    public class DefaultIdentifier : ClaimsIdentity, IIdentifier
    {
        public DefaultIdentifier(string accountId, string account)
        {
            AccountId = accountId;
            Account = account;
        }

        public string AccountId { get; set; }

        public string Account { get; set; }

    }
}
