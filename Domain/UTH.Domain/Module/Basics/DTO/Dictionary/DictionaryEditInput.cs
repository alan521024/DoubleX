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
    /// 数据字典输入
    /// </summary>
    public class DictionaryEditInput : DictionaryDTO, IInput
    {
    }

    /// <summary>
    /// 数据字典输入校验
    /// </summary>
    public class DictionaryEditInputValidator : DictionaryValidator<DictionaryEditInput>, IValidator<DictionaryEditInput>
    {
        public DictionaryEditInputValidator()
        {
            RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
                .NotNull().NotEmpty();
        }
    }
}
