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
    /// 人员信息
    /// </summary>
    [SugarTable("UC_Employe")]
    public class EmployeEntity : BaseFullEntity
    {
        /// <summary>
        /// 所属组织
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 织组电话
        /// </summary>
        public string Phone { get; set; }

    }
}
