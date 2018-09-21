namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 通知消息业务接口
    /// </summary>
    public interface INotificationService : IApplicationService
    {
        /// <summary>
        /// 消息发送
        /// </summary>
        NotificationOutput Send(NotificationInput input);
    }
}
