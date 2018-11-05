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
    public class UserSettingModel
    {
        /// <summary>
        /// 允许登录
        /// </summary>
        public bool AllowLogin { get; set; } = true;

        /// <summary>
        /// 允许注册
        /// </summary>
        public bool AllowRegist { get; set; } = true;

        /// <summary>
        /// 人员数量
        /// </summary>
        public int EmployeLimit { get; set; } = 0;
    }
}
