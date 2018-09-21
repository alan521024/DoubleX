namespace UTH.Module.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Reflection;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 消息通知 Aop
    /// </summary>
    public class NotificationInterceptor : INotificationInterceptor, IInterceptor
    {
        public NotificationInterceptor(IApplicationSession _session, INotificationService _notificationService)
        {
            session = _session;
            notificationService = _notificationService;
        }

        private IApplicationSession session { get; set; }
        private INotificationService notificationService { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var attrs = invocation.Method.GetCustomAttributes<NotificationAttribute>();

            //excute before
            InterceptBefore(invocation, attrs);

            //proceed
            invocation.Proceed();

            //excute after
            InterceptAfter(invocation, attrs);
        }

        private void InterceptBefore(IInvocation invocation,IEnumerable<NotificationAttribute> attrs)
        {
            if (attrs.IsEmpty())
                return;
        }

        private void InterceptAfter(IInvocation invocation, IEnumerable<NotificationAttribute> attrs)
        {
            if (attrs.IsEmpty())
                return;

            attrs.ToList().ForEach(x =>
            {
                NotificationSend(invocation, x);
            });
        }

        private void NotificationSend(IInvocation invocation, NotificationAttribute attr)
        {
            invocation.CheckNull();

            if (attr.IsEmpty())
                return;

            //发送
            if (attr.NotificationCategory == EnumNotificationCategory.Regist)
            {
                var input = invocation.Arguments.FirstOrDefault() as RegistInput;
                if (input.IsNull())
                    return;

                var model = new NotificationInput()
                {
                    Category = attr.NotificationCategory,
                    Type = attr.NotificationType,
                    Receiver = attr.NotificationType == EnumNotificationType.Sms ? input.Mobile : input.Email,
                    Content = Lang.userZhuCeChengGongXiaoXi
                };
                notificationService.Send(model);
            }
        }
    }
}
