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
    /// 应用版本信息
    /// </summary>
    [SugarTable("BAS_AppVersion")]
    public class AppVersionEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Descript { get; set; }

        /// <summary>
        /// 文件MD5(用于校验)
        /// </summary>
        public string FileMd5 { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseDt { get; set; }

        /// <summary>
        /// 更新类型
        /// </summary>
        public EnumUpdateType UpdateType { get; set; }

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
