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
    /// UTH Service 属性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ServiceAttribute : Attribute, IServiceAttribute
    {
        /// <summary>
        /// 是否启用Aop
        /// </summary>
        public bool IsAop { get; set; } = true;

        /// <summary>
        /// 是否启用认证授权
        /// </summary>
        public bool IsAuthorize { get; set; } = true;

        /// <summary>
        /// 权限列表以‘,’分割
        /// </summary>
        public string Permissions { get; set; }

    }
}
