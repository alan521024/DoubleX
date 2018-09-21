using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// 应用程序会话信息接口
    /// </summary>
    public interface IApplicationSession //ISessionService
    {
        /// <summary>
        /// 访问语言环境
        /// </summary>
        string Culture { get; set; }

        /// <summary>
        /// 应用程序Id
        /// </summary>
        string AppCode { get; set; }

        /// <summary>
        /// 客户端Ip
        /// </summary>
        string ClientIp { get; set; }

        /// <summary>
        /// Token票据
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// 会话账号
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// 手机号码(认证通过)
        /// </summary>
        string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址(认证通过)
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        string Role { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        int Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        Guid AccountId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        Guid TenantId { get; set; }
    }
}
