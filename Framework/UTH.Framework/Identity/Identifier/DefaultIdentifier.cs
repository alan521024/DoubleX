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
    using System.IO;

    /// <summary>
    /// UTH 身份信息对象 扩展
    /// </summary>
    [Serializable]
    public class DefaultIdentifier : ClaimsIdentity, IIdentifier, IIdentity
    {
        #region 构造方法

        public DefaultIdentifier() : base() { }
        public DefaultIdentifier(IIdentity identity) : base(identity)
        {
            Id = GuidHelper.Get(Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Id)?.Value);
            No = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.No)?.Value;
            Account = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Account)?.Value;
            Mobile = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Mobile)?.Value;
            Email = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Email)?.Value;
            Role = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Role)?.Value;
            RealName = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.RealName)?.Value;
            Organize = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Organize)?.Value;
            Employe = Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Employe)?.Value;
            TenantId = GuidHelper.Get(Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.TenantId)?.Value);
            Type = EnumsHelper.Get<EnumAccountType>(Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Type)?.Value);
            Status = EnumsHelper.Get<EnumAccountStatus>(Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Status)?.Value);
        }

        //public DefaultIdentifier(string authenticationType) : base(authenticationType) { }
        //public DefaultIdentifier(BinaryReader reader) : base(reader) { }
        //public DefaultIdentifier(IEnumerable<Claim> claims) : base(claims) { }
        //public DefaultIdentifier(IEnumerable<Claim> claims, string authenticationType) : base(claims, authenticationType) { }
        //public DefaultIdentifier(IIdentity identity, IEnumerable<Claim> claims) : base(identity, claims) { }
        //public DefaultIdentifier(string authenticationType, string nameType, string roleType) : base(authenticationType, nameType, roleType) { }
        //public DefaultIdentifier(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : base(claims, authenticationType, nameType, roleType) { }
        //public DefaultIdentifier(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : base(identity, claims, authenticationType, nameType, roleType) { }

        #endregion


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

    }
}
