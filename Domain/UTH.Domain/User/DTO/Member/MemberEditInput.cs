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
    /// 个人用户输入
    /// </summary>
    public class MemberEditInput : MemberDTO, IInput
    {

    }

    /// <summary>
    /// 个人用户输入校验
    /// </summary>
    public class MemberEditInputValidator : MemberValidator<MemberEditInput>, IValidator<MemberEditInput>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MemberEditInputValidator()
        {
            //rule...
        }
    }
}
