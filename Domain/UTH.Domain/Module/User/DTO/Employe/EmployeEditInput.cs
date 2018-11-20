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
    /// 人员输入
    /// </summary>
    public class EmployeEditInput : EmployeDTO, IInput
    {
        public int BatchStart { get; set; }
        public int BatchEnd { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// 人员输入校验
    /// </summary>
    public class EmployeEditInputValidator : EmployeValidator<EmployeEditInput>, IValidator<EmployeEditInput>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public EmployeEditInputValidator()
        {
            RuleFor(o => o.Code).Configure(x => x.PropertyName = Lang.sysBianHao)
                .NotNull().NotEmpty().MinimumLength(2);

            RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng);

            RuleFor(o => o.Password).Configure(x => x.PropertyName = Lang.userMiMa)
                .NotNull().NotEmpty().When(x => x.Id.IsEmpty() && x.Ids.IsEmpty());
        }
    }
}
