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
    /// 应用版本输入
    /// </summary>
    public class AppVersionEditInput : AppVersionDTO, IInput
    {
    }

    /// <summary>
    /// 应用版本输入校验
    /// </summary>
    public class AppVersionEditInputValidator : AbstractValidator<AppVersionEditInput>, IValidator<AppVersionEditInput>
    {
        public AppVersionEditInputValidator()
        {
            //RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
            //    .NotNull().NotEmpty();
        }
    }
}
