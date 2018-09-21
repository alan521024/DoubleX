using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework
{
    /// <summary>
    /// Token信息
    /// </summary>
    public class TokenModel : DefaultSession
    {
        /// <summary>
        /// TokenId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 刷新时间
        /// </summary>
        public DateTime LastDt { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiresDt { get; set; }

        /// <summary>
        /// 转为Claims列表
        /// </summary>
        /// <returns></returns>
        public Claim[] ToClaims()
        {
            return new Claim[] {
                new Claim(ClaimTypesExtend.Id, Id),
                new Claim(ClaimTypesExtend.AccountId, AccountId.FormatString()),
                new Claim(ClaimTypesExtend.Account, Account),
                new Claim(ClaimTypesExtend.Mobile, Mobile.IsEmpty()?"":Mobile),
                new Claim(ClaimTypesExtend.Email, Email.IsEmpty()?"":Email),
                new Claim(ClaimTypesExtend.RealName, RealName.IsEmpty()?Account:RealName),
                new Claim(ClaimTypesExtend.Role, Role.IsEmpty()?"":Role),
                new Claim(ClaimTypesExtend.Type, StringHelper.Get(Type)),
                new Claim(ClaimTypesExtend.Status, StringHelper.Get(Status)),
                new Claim(ClaimTypesExtend.TenantId, TenantId.FormatString())
                //new Claim(DbxClaimTypes.Nbf, $"{new DateTimeOffset(LastDt).ToUnixTimeSeconds()}"),
                //new Claim(DbxClaimTypes.Exp, $"{new DateTimeOffset(ExpiresDt).ToUnixTimeSeconds()}")
            };
        }
    }

}
