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
    /// 修改密码输入
    /// </summary>
    public class EditPwdInput : IInput
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 原密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string AffirmPassword { get; set; }
    }

    /// <summary>
    /// 修改密码校验
    /// </summary>
    public class EditPwdInputValidator : AbstractValidator<EditPwdInput>, IValidator<EditPwdInput>
    {
        public EditPwdInputValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty()
                .NotEmpty().WithMessage(Lang.userZhangHaoCuoWu);

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(Lang.userQingShuRuYouXiaoDeYuanMiMa);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(Lang.userQingShuRuYouXiaoDeXinMiMa);

            RuleFor(x => x.AffirmPassword)
                .NotEmpty().WithMessage(Lang.userQingShuRuYouXiaoDeQueRenMiMa)
                .Length(UserDomainValidator.PasswordLengthMin, UserDomainValidator.PasswordLengthMax).WithMessage(Lang.userQingShuRuYouXiaoDeQueRenMiMa)
                .Equal(x => x.NewPassword).WithMessage(Lang.userXinMiMaYuQueRenMiMaBuTong);
        }
    }
}
