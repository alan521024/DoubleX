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
    /// 账户刷新参数
    /// </summary>
    public class SignRefreshInput : IInput
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 账户刷新校验
    /// </summary>
    public class SignRefreshInputValidator : AbstractValidator<SignRefreshInput>, IValidator<SignRefreshInput>
    {
        public SignRefreshInputValidator()
        {
        }
    }
}
