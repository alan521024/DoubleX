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
    /// 验证码 校验Aop
    /// 验证码拦截器 在方法可设置 Verify及Remove操作 
    /// Verify 操作在方法执行前验证
    /// Remove 操作在方法执行后移除
    /// </summary>
    public class CaptchaVerifyInterceptor : ICaptchaVerifyInterceptor, IInterceptor
    {
        public CaptchaVerifyInterceptor(IApplicationSession _session, ICaptchaService _captchaService)
        {
            session = _session;
            captchaService = _captchaService;
        }

        private IApplicationSession session { get; set; }
        private ICaptchaService captchaService { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var attrs = invocation.Method.GetCustomAttributes<CaptchaVerifyAttribute>();

            ICaptchaVerifyInput input = null;
            foreach (var item in invocation.Arguments)
            {
                input = item as ICaptchaVerifyInput;
                if (!input.IsNull())
                    break;
            }

            //excute before
            InterceptBefore(invocation, attrs, input);

            //proceed
            invocation.Proceed();

            //excute after
            InterceptAfter(invocation, attrs, input);
        }

        private void InterceptBefore(IInvocation invocation, IEnumerable<CaptchaVerifyAttribute> attrs, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attrs.IsEmpty() || input.IsNull())
                return;

            var verifyAttrs = attrs.Where(x => x.CaptchaOperates.Where(t => t == EnumCaptchaOperate.Verify).Count() > 0).ToList();
            if (verifyAttrs.IsEmpty())
                return;

            verifyAttrs.ToList().ForEach(x =>
            {
                Verify(invocation, x, input);
            });
        }

        private void InterceptAfter(IInvocation invocation, IEnumerable<CaptchaVerifyAttribute> attrs, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attrs.IsEmpty() || input.IsNull())
                return;

            var removeAttrs = attrs.Where(x => x.CaptchaOperates.Where(t => t == EnumCaptchaOperate.Remove).Count() > 0).ToList();
            if (removeAttrs.IsEmpty())
                return;

            removeAttrs.ToList().ForEach(x =>
            {
                Remove(invocation, x, input);
            });
        }

        private void Verify(IInvocation invocation, CaptchaVerifyAttribute attr, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attr.IsNull() || input.IsNull())
                return;

            if (attr.CaptchaOperates.Where(t => t == EnumCaptchaOperate.Verify).Count() == 0)
                return;

            var model = new CaptchaVerifyInput()
            {
                Category = attr.NotificationCategory,
                Type = attr.NotificationType
            };

            if (attr.NotificationType == EnumNotificationType.Image)
            {
                model.Code = input.ImgCode;
                model.Tag = input.ImgCodeTag;
            }

            if (attr.NotificationType == EnumNotificationType.Sms)
            {
                model.Receiver = input.Mobile;
                model.Code = input.SmsCode;
                model.Tag = input.SmsCodeTag;
            }

            if (!captchaService.Verify(model))
            {
                if (attr.IsMust)
                    throw new DbxException(EnumCode.校验失败, Lang.userYanZhengMaCuoWu);
            }
        }

        private void Remove(IInvocation invocation, CaptchaVerifyAttribute attr, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attr.IsNull() || input.IsNull())
                return;

            if (attr.CaptchaOperates.Where(t => t == EnumCaptchaOperate.Remove).Count() == 0)
                return;

            var model = new CaptchaVerifyInput()
            {
                Category = attr.NotificationCategory,
                Type = attr.NotificationType
            };

            if (attr.NotificationType == EnumNotificationType.Image)
            {
                model.Code = input.ImgCode;
                model.Tag = input.ImgCodeTag;
            }

            if (attr.NotificationType == EnumNotificationType.Sms)
            {
                model.Receiver = input.Mobile;
                model.Code = input.SmsCode;
                model.Tag = input.SmsCodeTag;
            }

            if (!attr.IsMust && (model.Tag + model.Code + model.Receiver).IsEmpty())
            {
                return;
            }

            captchaService.Remove(model);
        }
    }
}
