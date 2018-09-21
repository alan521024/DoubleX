namespace UTH.Module.Core
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
    using UTH.Plug;
    using UTH.Plug.Notification;

    /// <summary>
    /// 验证码业务
    /// </summary>
    public class NotificationService : ApplicationService, INotificationService
    {
        /// <summary>
        /// 通知消息发送
        /// </summary>
        public NotificationOutput Send(NotificationInput input)
        {
            input.CheckNull();

            var output = new NotificationOutput() { Success = false };

            //default image 无发送 默认成功
            if (input.Type == EnumNotificationType.Default || input.Type == EnumNotificationType.Image)
            {
                output.Success = true;
            }

            //sms 短信
            if (input.Type == EnumNotificationType.Sms)
            {
                var smsService = EngineHelper.Resolve<ISmsService>();
                var result = smsService.Send(input.Receiver, input.Content);
                output.Success = result.Success;
                output.Message = result.Message;
            }

            //邮箱
            if (input.Type == EnumNotificationType.Email)
            {

            }

            return output;
        }
    }
}
