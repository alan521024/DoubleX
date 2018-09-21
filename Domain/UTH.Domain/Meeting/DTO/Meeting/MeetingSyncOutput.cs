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
    /// 会议系统同步返回
    /// </summary>
    public class MeetingSyncOutput : IOutput
    {
        public List<MeetingSyncModel> Records { get; set; }
        public List<MeetingSyncModel> Translations { get; set; }
    }
}
