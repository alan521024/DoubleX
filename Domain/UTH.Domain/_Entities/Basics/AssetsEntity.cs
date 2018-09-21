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
    /// 资源信息
    /// </summary>
    [SugarTable("BAS_Assets")]
    public class AssetsEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源MD5
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public EnumAssetsType AssetsType { get; set; }
    }
}
