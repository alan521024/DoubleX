namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 验证码校验输入
    /// </summary>
    public class CaptchaVerifyInput : CaptchaInput, IInput
    {
        public CaptchaVerifyInput() { }

        public CaptchaVerifyInput(EnumNotificationCategory category, EnumNotificationType type)
        {
            Category = category;
            Type = type;
        }

        public string Code { get; set; }
    }

    /// <summary>
    /// 验证码校验校验
    /// </summary>
    public class CaptchaVerifyInputValidator : AbstractValidator<CaptchaVerifyInput>, IValidator<CaptchaVerifyInput>
    {
        public CaptchaVerifyInputValidator()
        {
        }
    }
}
