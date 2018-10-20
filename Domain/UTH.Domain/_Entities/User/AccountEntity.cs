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
    /// 账号信息
    /// </summary>
    [SugarTable("UC_Account")]
    public class AccountEntity : BaseFullEntity
    {
        public AccountEntity()
        {
        }

        /// <summary>
        /// 账户类型(EnumAccountType)
        /// </summary>
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 账号编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 认证手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 认证邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateNo { get; set; }

        /// <summary>
        /// 手机是否认证
        /// </summary>
        public bool MobileAuth { get; set; }

        /// <summary>
        /// 邮箱是否认证
        /// </summary>
        public bool EmailAuth { get; set; }

        /// <summary>
        /// 证件是否认证
        /// </summary>
        public bool CertificateAuth { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 规范登录账号
        /// </summary>
        public string NormalizedAccount { get; set; }

        /// <summary>
        /// 规范邮箱地址
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 启用双因素认证(ref:http://blog.sina.com.cn/s/blog_3c9e07d40101agix.html)
        /// </summary>
        public bool IsTwoFactorEnabled { get; set; }

        /// <summary>
        /// 启用锁定
        /// </summary>
        public bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// 验证失败次数
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 安全相关信息快照。
        /// 应用场景，假如说用户修改了密码或者是修改了角色，退出等涉及到用户安全相关的时候，这个时候数据库这个值就会改变
        /// ref:https://www.cnblogs.com/savorboard/p/5422084.html
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 当用户被持久化存储到存储区时，必须更改的随机值
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 邀请人账号Id
        /// </summary>
        public Guid InviterId { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginLastDt { get; set; }

        /// <summary>
        /// 最后登录Ip
        /// </summary>
        public string LoginLastIp { get; set; }

        /// <summary>
        /// 账户状态(EnumAccountStatus)
        /// </summary>
        public EnumAccountStatus Status { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrganizeNo { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string EmployeNo { get; set; }
    }
}
