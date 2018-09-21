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
    /// 找回密码输入
    /// </summary>
    public class FindPwdInput : IInput
    {
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
        public string ImgCodeTag { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SmsCode { get; set; }
    }

    /// <summary>
    /// 找回修改校验
    /// </summary>
    public class FindPwdInputValidator : AccountValidator<FindPwdInput>, IValidator<FindPwdInput>
    {
        protected override Expression<Func<FindPwdInput, string>> mobileExpression => x => x.Mobile;
        protected override Expression<Func<FindPwdInput, string>> emailExpression => x => x.Email;
        protected override Expression<Func<FindPwdInput, string>> passwordExpression => x => x.Password;

        public FindPwdInputValidator()
        {
            CheckMobile();
            CheckEmail();
            CheckPassword().NotNull().NotEmpty();
        }
    }
}
