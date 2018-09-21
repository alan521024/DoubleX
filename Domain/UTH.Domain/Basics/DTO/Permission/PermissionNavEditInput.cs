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
    /// 权限导航编辑输入
    /// </summary>
    public class PermissionNavEditInput : PermissionNavDTO, IInput, IInputDelete, IInputUpdate, IInputTransaction
    {
        public List<Guid> Ids { get; set; }
        public bool IsTransaction { get; set; } = false;
    }

    /// <summary>
    /// 权限导航编辑输入校验
    /// </summary>
    public class PermissionNavEditInputValidator : AbstractValidator<PermissionNavEditInput>, IValidator<PermissionNavEditInput>
    {
        public PermissionNavEditInputValidator()
        {
        }

    }
}
