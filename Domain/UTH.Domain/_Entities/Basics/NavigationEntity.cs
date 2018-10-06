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
    /// 导航权限信息
    /// </summary>
    [SugarTable("BAS_Navigation")]
    public class NavigationEntity : BaseSimpleEntity, IEntityTree
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Guid Parent { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Paths { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 导航值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

    }
}
