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
    public class AssetsEntity : BaseEntity, ISoftDeleteEntity
    {
        /// <summary>
        /// 资源类型
        /// </summary>
        public EnumAssetsType AssetsType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源MD5
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// 资源大下
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 操作账号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 来源应用
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
