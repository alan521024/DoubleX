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
    public class TokenModel : IIdentifier
    {
        /// <summary>
        /// _key
        /// </summary>
        public string _Key { get; set; }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 账号编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 组织(编号)
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 人员(编号)
        /// </summary>
        public string Employe { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public EnumAccountStatus Status { get; set; }

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
                new Claim(ClaimTypesExtend._Key, _Key),
                new Claim(ClaimTypesExtend.Id, Id.FormatString()),
                new Claim(ClaimTypesExtend.Account, Account),
                new Claim(ClaimTypesExtend.Mobile, Mobile.IsEmpty()?"":Mobile),
                new Claim(ClaimTypesExtend.Email, Email.IsEmpty()?"":Email),
                new Claim(ClaimTypesExtend.Role, Role.IsEmpty()?"":Role),
                new Claim(ClaimTypesExtend.RealName, RealName.IsEmpty()?Account:RealName),
                new Claim(ClaimTypesExtend.Organize, Organize.IsEmpty()?"":Organize),
                new Claim(ClaimTypesExtend.Employe, Employe.IsEmpty()?"":Employe),
                new Claim(ClaimTypesExtend.Type, StringHelper.Get(Type)),
                new Claim(ClaimTypesExtend.Status, StringHelper.Get(Status)),
                new Claim(ClaimTypesExtend.TenantId, TenantId.FormatString())
                //new Claim(DbxClaimTypes.Nbf, $"{new DateTimeOffset(LastDt).ToUnixTimeSeconds()}"),
                //new Claim(DbxClaimTypes.Exp, $"{new DateTimeOffset(ExpiresDt).ToUnixTimeSeconds()}")
            };
        }
    }

}
