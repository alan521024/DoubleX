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
    /// 操作权限输入
    /// </summary>
    public class OperateEditInput : OperateDTO, IInput
    {
    }

    /// <summary>
    /// 操作权限输入校验
    /// </summary>
    public class OperateEditInputValidator : OperateValidator<OperateEditInput>, IValidator<OperateEditInput>
    {
        public OperateEditInputValidator()
        {
            RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
                .NotNull().NotEmpty();
        }
    }
}
