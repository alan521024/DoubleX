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
    /// 账号输入
    /// </summary>
    public class AccountEditInput : AccountDTO, IInput
    {

    }

    /// <summary>
    /// 账号输入校验
    /// </summary>
    public class AccountEditInputValidator : AccountValidator<AccountEditInput>, IValidator<AccountEditInput>
    {
        public AccountEditInputValidator()
        {
            //rule...
        }
    }
}
