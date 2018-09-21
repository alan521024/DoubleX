namespace UTH.Meeting.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Speech;

    /// <summary>
    /// 服务配置选项
    /// </summary>
    [Serializable]
    public class ServerOption
    {
        /// <summary>
        /// 会议配置
        /// </summary>
        public MeetingSettingModel MeetingSetting { get; set; } = new MeetingSettingModel();
    }
}
