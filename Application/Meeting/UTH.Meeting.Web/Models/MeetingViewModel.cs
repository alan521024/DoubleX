namespace UTH.Meeting.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.Helpers;
    using Senparc.Weixin.MP.Containers;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Module.Core;

    public class MeetingViewModel
    {
        /// <summary>
        /// Api Url
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Web Url
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 当前请求Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 会议信息
        /// </summary>
        public MeetingOutput Meeting { get; set; }

        /// <summary>
        /// 会议设置
        /// </summary>
        public MeetingSettingModel Setting { get; set; }

        /// <summary>
        /// 微信JS信息
        /// </summary>
        public JsSdkUiPackage WxJs { get; set; }
    }
}
