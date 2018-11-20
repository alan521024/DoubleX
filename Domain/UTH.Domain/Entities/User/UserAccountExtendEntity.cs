namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using SqlSugar;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 用户账号扩展信息
    /// </summary>
    public class UserAccountExtendEntity : BaseFullEntity
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Account { get; set; }

        /// <summary>
        /// 认证手机
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Mobile { get; set; }

        /// <summary>
        /// 认证邮箱
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Email { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CertificateNo { get; set; }

        /// <summary>
        /// 手机是否认证
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool MobileAuth { get; set; }

        /// <summary>
        /// 邮箱是否认证
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool EmailAuth { get; set; }

        /// <summary>
        /// 证件是否认证
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool CertificateAuth { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RealName { get; set; }

        /// <summary>
        /// 邀请人账号Id
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Guid InviterId { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int LoginCount { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DateTime LoginLastDt { get; set; }

        /// <summary>
        /// 最后登录Ip
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string LoginLastIp { get; set; }

        /// <summary>
        /// 账户状态(EnumAccountStatus)
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public EnumAccountStatus Status { get; set; }
    }
}
