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
        public const string _Key = JwtRegisteredClaimNames.Jti;

        /// <summary>
        /// 账号Id
        /// </summary>
        public const string Id = "id"; //ClaimTypes.Sid

        /// <summary>
        /// 编号
        /// </summary>
        public const string No = "no";

        /// <summary>
        /// 账号
        /// </summary>
        public const string Account = JwtRegisteredClaimNames.Sub;

        /// <summary>
        /// 手机号码
        /// </summary>
        public const string Mobile = "mobile";

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// 角色
        /// </summary>
        public const string Role = ClaimTypes.Role;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public const string RealName = "realName";

        /// <summary>
        /// 组织编号
        /// </summary>
        public const string Organize = "organize";

        /// <summary>
        /// 员工编号
        /// </summary>
        public const string Employe = "employe";

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

        /// <summary>
        /// 在该时间之前不可用(UnixTimeSeconds)
        /// </summary>
        public const string Nbf = JwtRegisteredClaimNames.Nbf;

        /// <summary>
        ///在该时间之后不可用(UnixTimeSeconds)，超时时间
        /// </summary>
        public const string Exp = JwtRegisteredClaimNames.Exp;
        
        #region  JWT Claims

        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().FormatString()),
        //new Claim(JwtRegisteredClaimNames.Sub, account),
        //new Claim(ClaimTypes.Sid, id.FormatString()),
        //new Claim(ClaimTypes.MobilePhone, mobile.IsEmpty()?"":mobile),
        //new Claim(ClaimTypes.Email, email.IsEmpty()?"":email),
        //new Claim(ClaimTypes.Name, realName.IsEmpty()?"":realName),
        //new Claim(ClaimTypes.Role, role.IsEmpty()?"":role),
        //new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(nowDt).ToUnixTimeSeconds()}"),
        //new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(expiresDt).ToUnixTimeSeconds()}")

        //Header//头信息 
        //{   
        //    "alg": "HS256",//签名或摘要算法 
        //    "typ": "JWT"//token类型 
        //} 
        //Playload//荷载信息 
        //{   
        //    "iss": "token-server",//jwt签发者
        //    "aud": "web-server-1"//接收方, 
        //    "sub": "wangjie",//jwt所面向的用户,用户名
        //    "exp": "Mon Nov 13 15:28:41 CST 2017",//过期时间,jwt的过期时间，这个过期时间必须大于签发时间
        //    "nbf": "Mon Nov 13 15:40:12 CST 2017",//这个时间之前token不可用 
        //    "iat": "Mon Nov 13 15:20:41 CST 2017",//jwt的签发时间
        //    "jti": "0023",jwt的唯一身份标识，主要用来作为一次性token，从而回避重放攻击 //令牌id标识，
        //    "claim": {“auth”:”ROLE_ADMIN”}//访问主张,*****待确认使用方式
        //} 
        //Signature//签名信息 签名或摘要算法（ base64urlencode（Header）， Base64urlencode（Playload）， secret-key ）


        #endregion

    }
}
