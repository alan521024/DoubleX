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
    /// 验证码发送输入
    /// </summary>
    public class CaptchaSendInput : CaptchaInput, IInput
    {
        public CaptchaSendInput() { }

        public CaptchaSendInput(EnumNotificationCategory category, EnumNotificationType type, string receiver = null)
        {
            Category = category;
            Type = type;
            Receiver = receiver;
        }

        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; } = 6;
    }

    /// <summary>
    /// 验证码发送校验
    /// </summary>
    public class CaptchaSendInputValidator : AbstractValidator<CaptchaSendInput>, IValidator<CaptchaSendInput>
    {
        public CaptchaSendInputValidator()
        {
        }
    }
}
