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
    /// 会议翻译输入
    /// </summary>
    public class MeetingTranslationEditInput : MeetingTranslationDTO, IInput
    {

    }

    /// <summary>
    /// 会议翻译输入校验
    /// </summary>
    public class MeetingTranslationEditInputValidator : MeetingValidator<MeetingTranslationEditInput>, IValidator<MeetingTranslationEditInput>
    {
        public MeetingTranslationEditInputValidator()
        {
            //RuleFor(o => o.Name).Configure(x => x.PropertyName = Lang.sysMingCheng)
            //    .NotNull().NotEmpty();
        }
    }
}
