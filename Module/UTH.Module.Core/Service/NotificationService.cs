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
    /// 通知领域服务
    /// </summary>
    public class NotifyService : DomainService, INotifyService
    {
        ISmsService smsService;

        public NotifyService(IApplicationSession session, ICachingService caching, ISmsService smsService) : base(session, caching)
        {
            this.smsService = smsService;
        }

        /// <summary>
        /// 通知发送
        /// </summary>
        public bool Send(EnumNotifyCategory category, EnumNotifyMode mode, string sender, string receiver, string content)
        {
            content = FormatContent(category, mode, content);
            content.CheckEmpty();

            switch (mode)
            {
                case EnumNotifyMode.短信:
                    if (receiver.IsEmpty())
                    {
                        //TODO:Msg 通知接收人错误
                    }
                    return smsService.Send(receiver, content).Success;
                default:
                    return true;
            }
        }

        /// <summary>
        /// 默认内容
        /// </summary>
        /// <param name="category"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private string FormatContent(EnumNotifyCategory category, EnumNotifyMode mode, string content)
        {
            if (content.IsNull())
            {
                content = "";
            }

            if (category == EnumNotifyCategory.注册验证)
            {
                content.CheckEmpty();
                content = "注册 验证码为 " + content + " ";
            }

            if (category == EnumNotifyCategory.找回密码验证)
            {
                content.CheckEmpty();
                content = "找回密码 验证码为 " + content + " ";
            }

            return content;
        }
    }
}
