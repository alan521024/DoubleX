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
    /// 资源基本信息(DTO)
    /// </summary>
    [Serializable]
    public class AssetsDTO : IKeys, IOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ids
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public EnumAssetsType AssetsType { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 资源大下
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 操作账号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 来源应用
        /// </summary>
        public string AppCode { get; set; }
    }
}
