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
    public class NotificationAttribute : Attribute
    {
        public NotificationAttribute() { }

        public NotificationAttribute(EnumNotificationCategory notificationCategory, EnumNotificationType notificationType)
        {
            NotificationCategory = notificationCategory;
            NotificationType = notificationType;
        }

        /// <summary>
        /// 验证码类别
        /// </summary>
        public EnumNotificationCategory NotificationCategory { get; set; }

        /// <summary>
        /// 验证码类型
        /// </summary>
        public EnumNotificationType NotificationType { get; set; }

        /// <summary>
        /// 自定义内容
        /// </summary>
        public string Content { get; set; }
    }
}
