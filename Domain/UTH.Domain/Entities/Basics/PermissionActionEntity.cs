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
    /// 权限操作信息
    /// </summary>
    [SugarTable("BAS_PermissionAction")]
    public class PermissionActionEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 所属导航
        /// </summary>
        public Guid NavigateId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

    }
}
