namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 用户设置信息
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// 个人用户设置
        /// </summary>
        public UserSettingModel Member { get; set; } = new UserSettingModel();

        /// <summary>
        /// 组织用户设置
        /// </summary>
        public UserSettingModel Origin { get; set; } = new UserSettingModel();

        /// <summary>
        /// 人员用户设置
        /// </summary>
        public UserSettingModel Employe { get; set; } = new UserSettingModel();

    }
}
