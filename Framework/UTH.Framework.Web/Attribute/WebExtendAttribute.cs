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
    /// 控制器/方法 Web扩展 属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WebExtendAttribute : Attribute
    {
        private bool? _isExceptionWrapper;
        private bool? _isResultWrapper;
        private bool? _isAuthorize;

        /// <summary>
        /// 是否异常返回格式包裹
        /// </summary>
        public bool IsExceptionWrapper
        {
            get => _isExceptionWrapper ?? true;
            set => _isExceptionWrapper = value;
        }

        /// <summary>
        /// 是否返回结果格式包裹
        /// </summary>
        public bool IsResultWrapper
        {
            get => _isResultWrapper ?? true;
            set => _isResultWrapper = value;
        }

        /// <summary>
        /// 是否执行授权
        /// </summary>
        public bool IsAuthorize
        {
            get => _isAuthorize ?? true;
            set => _isAuthorize = value;
        }
    }
}
