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
    using System.Linq.Expressions;

    /// <summary>
    /// 账户注册输入
    /// </summary>
    public class RegistInput : IInput, ICaptchaVerifyInput
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 组织名称(如传入非空则视为组织注册)
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 图片验证码
        /// </summary>
        public string ImgCode { get; set; }

        /// <summary>
        /// 图片验证码标识
        /// </summary>
        public string ImgCodeKey { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SmsCode { get; set; }

        /// <summary>
        ///  短信验证码标识
        /// </summary>
        public string SmsCodeKey { get; set; }
    }

    /// <summary>
    /// 账户注册校验
    /// </summary>
    public class RegistInputValidator : AccountValidator<RegistInput>, IValidator<RegistInput>
    {
        protected override Expression<Func<RegistInput, string>> accountExpression => x => x.Account;
        protected override Expression<Func<RegistInput, string>> mobileExpression => x => x.Mobile;
        protected override Expression<Func<RegistInput, string>> emailExpression => x => x.Email;
        protected override Expression<Func<RegistInput, string>> passwordExpression => x => x.Password;

        public RegistInputValidator()
        {
            RuleFor(x => x.Account + x.Email + x.Mobile)
                .NotEmpty()
                .WithMessage(Lang.userQingShuRuZhangHaoShouJiHaoMaYouXiangDiZhi);

            CheckAccount();
            CheckMobile();
            CheckEmail();
            CheckPassword().NotNull().NotEmpty();
        }
    }
}
