namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 安全码信息
    /// </summary>
    public class SecurityCodeModel
    {
        /// <summary>
        /// KEY
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 类型(暂不返回)
        /// </summary>
        //public EnumSecurityCodeType Type { get; set; }

        /// <summary>
        /// 码值
        /// </summary>
        public string Code { get; set; }
    }
}
