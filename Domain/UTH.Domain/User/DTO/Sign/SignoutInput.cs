namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 账户签出参数
    /// </summary>
    public class SignOutInput : IInput
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 账户签出校验
    /// </summary>
    public class SignOutInputValidator : AbstractValidator<SignOutInput>, IValidator<SignOutInput>
    {
        public SignOutInputValidator()
        {
        }
    }
}
