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
    /// 应用程序编辑输入
    /// </summary>
    public class AppEditInput : AppBase, IInput, IInputDelete, IInputUpdate
    {
        public List<Guid> Ids { get; set; }
    }

    /// <summary>
    /// 应用程序编辑输入校验
    /// </summary>
    public class AppEditInputValidator : AppValidator<AppEditInput>, IValidator<AppEditInput>
    {
        public AppEditInputValidator()
        {
            RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
                .NotNull().NotEmpty().When(x => x.Ids == null);

            RuleFor(o => o.Code).Configure(x => x.PropertyName = Lang.sysBianMa)
                .NotNull().NotEmpty().Length(6).When(x => x.Ids == null);
        }
    }
}
