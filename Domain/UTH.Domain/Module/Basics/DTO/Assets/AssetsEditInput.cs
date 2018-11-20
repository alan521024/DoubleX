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
    /// 资源输入
    /// </summary>
    public class AssetsEditInput : AssetsDTO, IInput
    {
    }

    /// <summary>
    /// 资源输入校验
    /// </summary>
    public class AssetsEditInputValidator : AssetsValidator<AssetsEditInput>, IValidator<AssetsEditInput>
    {
        public AssetsEditInputValidator()
        {
            RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
                .NotNull().NotEmpty();
        }
    }
}
