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
    /// UTH 身份信息对象 扩展操作
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        public static DefaultIdentifier GetUserIdentifierOrNull(this IIdentity identity)
        {
            identity.CheckNull();

            var accountId = identity.GetAccountId();
            if (accountId == null)
            {
                return null;
            }

            return new DefaultIdentifier(accountId, identity.GetAccount());
        }

        public static string GetAccountId(this IIdentity identity)
        {
            identity.CheckNull();

            var claimsIdentity = identity as ClaimsIdentity;

            var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypesExtend.Account);
            if (userIdOrNull == null || (userIdOrNull != null && userIdOrNull.Value.IsEmpty()))
            {
                return null;
            }

            return userIdOrNull.Value;
        }

        public static string GetAccount(this IIdentity identity)
        {
            identity.CheckNull();

            var claimsIdentity = identity as ClaimsIdentity;

            var accountOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypesExtend.Account);
            if (accountOrNull == null || (accountOrNull != null && accountOrNull.Value.IsEmpty()))
            {
                return null;
            }
            return accountOrNull.Value;
        }

    }
}
