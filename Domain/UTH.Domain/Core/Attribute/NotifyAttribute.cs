namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 通知消息 属性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NotifyAttribute : Attribute
    {
        public NotifyAttribute() { }

        public NotifyAttribute(EnumNotifyCategory category, EnumNotifyMode mode)
        {
            Category = category;
            Mode = mode;
        }

        /// <summary>
        /// 通知类型
        /// </summary>
        public EnumNotifyCategory Category { get; set; }

        /// <summary>
        /// 通知方式
        /// </summary>
        public EnumNotifyMode Mode { get; set; }

        /// <summary>
        /// 自定义内容
        /// </summary>
        public string Content { get; set; }
    }
}
