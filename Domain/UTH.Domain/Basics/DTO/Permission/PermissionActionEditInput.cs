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
    /// 权限操作编辑输入
    /// </summary>
    public class PermissionActionEditInput : PermissionNavDTO, IInput, IInputDelete, IInputUpdate, IInputTransaction
    {
        public List<Guid> Ids { get; set; }
        public bool IsTransaction { get; set; } = false;
    }

    /// <summary>
    /// 权限操作编辑输入校验
    /// </summary>
    public class PermissionActionEditInputValidator : AbstractValidator<PermissionActionEditInput>, IValidator<PermissionActionEditInput>
    {
        public PermissionActionEditInputValidator()
        {
        }

    }
}
