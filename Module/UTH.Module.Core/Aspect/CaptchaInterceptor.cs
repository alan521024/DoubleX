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

    /// <summary>
    /// 验证码 校验Aop
    /// 验证码拦截器 在方法可设置 Verify及Remove操作 
    /// Verify 操作在方法执行前验证
    /// Remove 操作在方法执行后移除
    /// </summary>
    public class CaptchaInterceptor : ICaptchaInterceptor, IInterceptor
    {
        IApplicationSession session { get; set; }
        ISecurityCodeService securityCodeService { get; set; }

        public CaptchaInterceptor(ISecurityCodeService _securityCodeService, IApplicationSession _session)
        {
            securityCodeService = _securityCodeService;
            session = _session;
        }

        const EnumSecurityCodeType defaultCodeType = EnumSecurityCodeType.Random;

        public void Intercept(IInvocation invocation)
        {
            var attrs = invocation.Method.GetCustomAttributes<CaptchaAttribute>();

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

        private void InterceptBefore(IInvocation invocation, IEnumerable<CaptchaAttribute> attrs, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attrs.IsEmpty() || input.IsNull())
                return;

            var verifyAttrs = attrs.Where(x => x.Operates.Where(t => t == EnumOperate.校验).Count() > 0).ToList();
            if (verifyAttrs.IsEmpty())
                return;

            verifyAttrs.ToList().ForEach(x =>
            {
                Verify(invocation, x, input);
            });
        }

        private void InterceptAfter(IInvocation invocation, IEnumerable<CaptchaAttribute> attrs, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attrs.IsEmpty() || input.IsNull())
                return;

            var removeAttrs = attrs.Where(x => x.Operates.Where(t => t == EnumOperate.删除).Count() > 0).ToList();
            if (removeAttrs.IsEmpty())
                return;

            removeAttrs.ToList().ForEach(x =>
            {
                Remove(invocation, x, input);
            });
        }

        private void Verify(IInvocation invocation, CaptchaAttribute attr, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attr.IsNull() || input.IsNull())
                return;

            if (attr.Operates.Where(t => t == EnumOperate.校验).Count() == 0)
                return;

            string key = string.Empty, code = string.Empty;

            if (attr.Mode == EnumCaptchaMode.Image)
            {
                key = input.ImgCodeKey;
                code = input.ImgCode;
            }

            if (attr.Mode == EnumCaptchaMode.Sms)
            {
                key = input.SmsCodeKey;
                code = input.SmsCode;
            }

            if (!attr.IsMust && (key + code).IsEmpty())
            {
                return;
            }

            if (!securityCodeService.Verify(defaultCodeType, key, code))
            {
                if (attr.IsMust)
                    throw new DbxException(EnumCode.校验失败, Lang.userYanZhengMaCuoWu);
            }
        }

        private void Remove(IInvocation invocation, CaptchaAttribute attr, ICaptchaVerifyInput input)
        {
            invocation.CheckNull();

            if (attr.IsNull() || input.IsNull())
                return;

            if (attr.Operates.Where(t => t == EnumOperate.删除).Count() == 0)
                return;

            string key = string.Empty, code = string.Empty;

            if (attr.Mode == EnumCaptchaMode.Image)
            {
                key = input.ImgCodeKey;
                code = input.ImgCode;
            }

            if (attr.Mode == EnumCaptchaMode.Sms)
            {
                key = input.SmsCodeKey;
                code = input.SmsCode;
            }

            if (!attr.IsMust && (key + code).IsEmpty())
            {
                return;
            }

            securityCodeService.Remove(defaultCodeType, key);
        }
    }
}
