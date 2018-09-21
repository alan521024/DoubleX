namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    
    /// <summary>
    /// 输入基本信息
    /// </summary>
    public class BaseInput
    {
        /// <summary>
        /// 访问会话
        /// </summary>
        public string Session { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 客户端信息
        /// </summary>
        public string ClientAgent { get; set; }

        /// <summary>
        /// 访问用户(根据Token解析)
        /// </summary>
        public string User { get; set; }

    }
}
