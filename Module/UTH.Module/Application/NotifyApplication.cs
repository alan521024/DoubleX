namespace UTH.Module
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
    using UTH.Domain;

    /// <summary>
    /// 验证码业务
    /// </summary>
    public class NotifyApplication : ApplicationService, INotifyApplication
    {
        INotifyService notifyService;

        public NotifyApplication(INotifyService notifyService, IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {
            this.notifyService = notifyService;
        }

        /// <summary>
        /// 通知消息发送
        /// </summary>
        public NotifyOutput Send(NotifyInput input)
        {
            input.CheckNull();

            var result = new NotifyOutput()
            {
                Success = false,
                Message = ""
            };

            if (notifyService.Send(input.Category, input.Mode, sender: input.Sender, receiver: input.Receiver, content: input.Content))
            {
                result.Success = true;
            }
            else
            {
                result.Message = "error";
            }
            return result;
        }
    }
}
