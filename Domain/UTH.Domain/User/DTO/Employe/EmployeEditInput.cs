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
    /// 人员编辑输入
    /// </summary>
    public class EmployeEditInput : EmployeDTO, IInput, IInputDelete, IInputUpdate
    {
        public List<Guid> Ids { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// 会议输入校验
    /// </summary>
    public class EmployeEditInputValidator : EmployeValidator<EmployeEditInput>, IValidator<EmployeEditInput>
    {
        public EmployeEditInputValidator()
        {
            //rule...
        }
    }
}
