namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using SqlSugar;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 应用配置信息
    /// </summary>
    [SugarTable("BAS_AppSetting")]
    public class AppSettingEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// 用户设置(JSON)
        /// </summary>
        public string UserJson { get; set; }

        /// <summary>
        /// 用户设置信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public UserSetting User { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string AppName { get; set; }

        /// <summary>
        /// 应用程序Code
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string AppCode { get; set; }
    }
}
