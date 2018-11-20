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
    /// 账户签入参数
    /// </summary>
    public class SignInInput : IInput
    {
        /// <summary>
        /// 用户名(可为账号/手机号码/邮箱地址)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 账号编号
        /// </summary>
        public string Num { get; set; }

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
        /// 图片验证码
        /// </summary>
        public string ImgCode { get; set; }

        /// <summary>
        /// 图片验证码标识
        /// </summary>
        public string ImgCodeKey { get; set; }
    }

    /// <summary>
    /// 账户签入校验
    /// </summary>
    public class SignInInputValidator : AccountValidator<SignInInput>, IValidator<SignInInput>
    {
        protected override Expression<Func<SignInInput, string>> accountExpression => x => x.Account;
        protected override Expression<Func<SignInInput, string>> mobileExpression => x => x.Mobile;
        protected override Expression<Func<SignInInput, string>> emailExpression => x => x.Email;
        protected override Expression<Func<SignInInput, string>> passwordExpression => x => x.Password;

        public SignInInputValidator()
        {
            RuleFor(x => x.UserName + x.Account + x.Email + x.Mobile)
                .NotEmpty()
                .WithMessage(Lang.userQingShuRuZhangHaoShouJiHaoMaYouXiangDiZhi);

            CheckAccount();
            CheckMobile();
            CheckEmail();
            CheckPassword().NotNull().NotEmpty();
        }
    }
}
