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
    /// 操作权限信息
    /// </summary>
    [SugarTable("BAS_Operate")]
    public class OperateEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 所属导航
        /// </summary>
        public Guid NavigateId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
