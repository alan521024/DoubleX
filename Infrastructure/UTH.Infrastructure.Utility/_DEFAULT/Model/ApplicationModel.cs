using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 应用Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 应用类型
        /// </summary>
        public EnumAppType AppType { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public ApplicationVersion Versions { get; set; }
    }
}
