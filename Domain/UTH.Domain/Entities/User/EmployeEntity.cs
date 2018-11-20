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
    /// 组织成员信息
    /// </summary>
    [SugarTable("UC_Employe")]
    public class EmployeEntity : UserAccountExtendEntity
    {
        /// <summary>
        /// 所属组织
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 账号编号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string No { get; set; }

    }
}
