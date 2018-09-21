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
    using UTH.Framework;

    /// <summary>
    /// 验证码校验 属性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CaptchaVerifyAttribute : Attribute
    {
        public CaptchaVerifyAttribute() { }

        public CaptchaVerifyAttribute(EnumNotificationCategory notificationCategory, EnumNotificationType notificationType, params EnumCaptchaOperate[] captchaOperates)
        {
            NotificationCategory = notificationCategory;
            NotificationType = notificationType;
            CaptchaOperates = !captchaOperates.IsEmpty() ? captchaOperates.ToList() : new List<EnumCaptchaOperate>();
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
        /// 验证码操作列表
        /// </summary>
        public List<EnumCaptchaOperate> CaptchaOperates { get; set; } = new List<EnumCaptchaOperate>();

        /// <summary>
        /// 判断input tag 及 code 为 空 '' 时，是否校验
        /// </summary>
        public bool IsMust { get; set; } = false;

        /// <summary>
        /// 自定义内容
        /// </summary>
        public string Content { get; set; }

    }
}
