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
    /// 应用程序基本信息(DTO)
    /// </summary>
    public class AppDTO: IKeys, IOutput
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
        /// 应用类型名称
        /// </summary>
        public string AppTypeName { get { return AppType.GetName(); } }
    }
}
