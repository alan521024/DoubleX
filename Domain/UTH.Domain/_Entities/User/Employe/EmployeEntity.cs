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
    /// 组员信息
    /// </summary>
    [SugarTable("UC_Employe")]
    public class EmployeEntity : BaseFullEntity
    {
        /// <summary>
        /// 所属组员UUID
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 唯一标识(组员简称或标识符号)
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// 组员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 织组电话
        /// </summary>
        public string Phone { get; set; }
    }
}
