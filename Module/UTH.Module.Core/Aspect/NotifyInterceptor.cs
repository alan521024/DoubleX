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
    /// 通知消息 Aop
    /// </summary>
    public class NotifyInterceptor : INotifyInterceptor, IInterceptor
    {
        public NotifyInterceptor(INotifyService _notificationService, IApplicationSession _session)
        {
            session = _session;
            notificationService = _notificationService;
        }

        private IApplicationSession session { get; set; }
        private INotifyService notificationService { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var attrs = invocation.Method.GetCustomAttributes<NotifyAttribute>();

            //excute before
            InterceptBefore(invocation, attrs);

            //proceed
            invocation.Proceed();

            //excute after
            InterceptAfter(invocation, attrs);
        }

        private void InterceptBefore(IInvocation invocation, IEnumerable<NotifyAttribute> attrs)
        {
            if (attrs.IsEmpty())
                return;
        }

        private void InterceptAfter(IInvocation invocation, IEnumerable<NotifyAttribute> attrs)
        {
            if (attrs.IsEmpty())
                return;

            attrs.ToList().ForEach(x =>
            {
                NotificationSend(invocation, x);
            });
        }

        private void NotificationSend(IInvocation invocation, NotifyAttribute attr)
        {
            invocation.CheckNull();

            if (attr.IsEmpty())
                return;

            //发送
            if (attr.Category == EnumNotifyCategory.注册结果)
            {
                var input = invocation.Arguments.FirstOrDefault() as RegistInput;
                if (input.IsNull())
                    return;

                notificationService.Send(attr.Category, attr.Mode, null, receiver: (attr.Mode == EnumNotifyMode.短信 ? input.Mobile : input.Email), content: Lang.userZhuCeChengGongXiaoXi);
            }
        }
    }
}
