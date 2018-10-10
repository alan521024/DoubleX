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
    /// 应用程序信息
    /// </summary>
    [SugarTable("BAS_App")]
    public class AppEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        public EnumAppType AppType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 应用编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 应用Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 应用版本(列忽略)
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<AppVersionEntity> Versions { get; set; } = new List<AppVersionEntity>();
    }
}
