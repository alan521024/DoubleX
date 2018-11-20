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
    public interface INotifyApplication : IApplicationService
    {
        /// <summary>
        /// 通知发送
        /// </summary>
        NotifyOutput Send(NotifyInput input);
    }
}
