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
    using System.IdentityModel.Tokens.Jwt;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// ClaimsTypes 扩展(ClaimTypes / 身份信息元素类型)
    /// see all : System.Security.Claims.ClaimTypes 
    /// </summary>
    public static class ClaimTypesExtend
    {
        /// <summary>
        /// Token唯一标识
        /// </summary>
        public const string Id = JwtRegisteredClaimNames.Jti;

        /// <summary>
        /// 账号Id
        /// </summary>
        public const string AccountId = ClaimTypes.Sid;

        /// <summary>
        /// 账号
        /// </summary>
        public const string Account = JwtRegisteredClaimNames.Sub;

        /// <summary>
        /// 手机号码
        /// </summary>
        public const string Mobile = ClaimTypes.MobilePhone;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public const string Email = ClaimTypes.Email;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public const string RealName = ClaimTypes.Name;

        /// <summary>
        /// 角色
        /// </summary>
        public const string Role = ClaimTypes.Role;

        /// <summary>
        /// 在该时间之前不可用(UnixTimeSeconds)
        /// </summary>
        public const string Nbf = JwtRegisteredClaimNames.Nbf;

        /// <summary>
        ///在该时间之后不可用(UnixTimeSeconds)，超时时间
        /// </summary>
        public const string Exp = JwtRegisteredClaimNames.Exp;
        
        /// <summary>
        /// Token
        /// </summary>
        public const string Token = "token";

        /// <summary>
        /// Type
        /// </summary>
        public const string Type = "type";

        /// <summary>
        /// Status
        /// </summary>
        public const string Status = "status";

        /// <summary>
        /// TenantId
        /// </summary>
        public const string TenantId = "tenantId";

        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().FormatString()),
        //new Claim(JwtRegisteredClaimNames.Sub, account),
        //new Claim(ClaimTypes.Sid, id.FormatString()),
        //new Claim(ClaimTypes.MobilePhone, mobile.IsEmpty()?"":mobile),
        //new Claim(ClaimTypes.Email, email.IsEmpty()?"":email),
        //new Claim(ClaimTypes.Name, realName.IsEmpty()?"":realName),
        //new Claim(ClaimTypes.Role, role.IsEmpty()?"":role),
        //new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(nowDt).ToUnixTimeSeconds()}"),
        //new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(expiresDt).ToUnixTimeSeconds()}")
    }
}
