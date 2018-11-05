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
    /// 应用设置输入
    /// </summary>
    public class AppSettingEditInput : AppSettingDTO, IInput
    {

    }

    /// <summary>
    /// 应用设置输入校验
    /// </summary>
    public class AppSettingEditInputValidator : AbstractValidator<AppSettingEditInput>, IValidator<AppSettingEditInput>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AppSettingEditInputValidator()
        {
            //RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
            //    .NotNull().NotEmpty();
        }
    }
}
