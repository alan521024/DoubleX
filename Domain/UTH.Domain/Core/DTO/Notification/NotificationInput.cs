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
    /// 通知信息基类
    /// </summary>
    public class NotificationInput : IInput
    {
        public EnumNotificationCategory Category { get; set; }
        public EnumNotificationType Type { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
    }
}
