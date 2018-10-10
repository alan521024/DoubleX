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
    /// 组织编辑输入
    /// </summary>
    public class OrganizeEditInput : OrganizeBase, IInput, IInputDelete, IInputUpdate
    {
        public List<Guid> Ids { get; set; }
    }

    /// <summary>
    /// 会议输入校验
    /// </summary>
    public class OrganizeEditInputValidator : OrganizeValidator<OrganizeEditInput>, IValidator<OrganizeEditInput>
    {
        public OrganizeEditInputValidator()
        {
            //rule...
        }
    }
}
