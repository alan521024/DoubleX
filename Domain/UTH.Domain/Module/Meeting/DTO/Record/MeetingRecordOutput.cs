namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 会议记录输出信息
    /// </summary>
    public class MeetingRecordOutput : MeetingRecordDTO, IOutput
    {
        /// <summary>
        /// 翻译记录
        /// </summary>
        public List<MeetingTranslationDTO> Translations { get; set; }
    }
}
