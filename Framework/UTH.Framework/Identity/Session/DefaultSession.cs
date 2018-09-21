using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// 默认会话信息
    /// </summary>
    [Serializable]
    public class DefaultSession : IApplicationSession
    {
        /// <summary>
        /// 访问语言环境
        /// </summary>
        public virtual string Culture { get; set; }

        /// <summary>
        /// 应用程序Id
        /// </summary>
        public virtual string AppCode { get; set; }

        /// <summary>
        /// 客户端Ip
        /// </summary>
        public virtual string ClientIp { get; set; }

        /// <summary>
        /// 会话Token
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// 会话账号
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 手机号码(认证通过)
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址(认证通过)
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        public virtual Guid AccountId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid TenantId { get; set; }
    }
}
