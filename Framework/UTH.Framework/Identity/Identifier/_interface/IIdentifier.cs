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
    public interface IIdentifier
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 账号编号
        /// </summary>
        string No { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        string Role { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        string RealName { get; set; }

        /// <summary>
        /// 组织(编号)
        /// </summary>
        string Organize { get; set; }

        /// <summary>
        /// 员工(编号)
        /// </summary>
        string Employe { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        Guid TenantId { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        EnumAccountType Type { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        EnumAccountStatus Status { get; set; }
    }
}
