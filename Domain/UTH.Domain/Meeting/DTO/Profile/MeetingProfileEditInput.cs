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
    /// 会议编辑输入
    /// </summary>
    public class MeetingProfileEditInput : MeetingProfileBase, IInput, IInputDelete, IInputUpdate, IInputTransaction
    {
        public List<Guid> Ids { get; set; }
        public bool IsTransaction { get; set; } = false;
        
        /// <summary>
        /// 当前所处会议ID(将同步更新所在会议配置) 
        /// </summary>
        public Guid MeetingId { get; set; }
    }

    /// <summary>
    /// 会议输入校验
    /// </summary>
    public class MeetingProfileEditInputValidator : AbstractValidator<MeetingProfileEditInput>, IValidator<MeetingProfileEditInput>
    {
        public MeetingProfileEditInputValidator()
        {
        }
    }
}
