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
    /// 验证码验证信息基类
    /// </summary>
    public class CaptchaInput : IInput
    {
        /// <summary>
        /// 所属分类
        /// </summary>
        public EnumNotificationCategory Category { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public EnumNotificationType Type { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }
    }
}
