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
    /// 组织信息(代理员/个体工商/公司企业/团队组织)
    /// </summary>
    [SugarTable("UC_Organize")]
    public class OrganizeEntity : BaseFullEntity
    {
        /// <summary>
        /// 唯一标识(组织简称或标识符号)
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 织组电话
        /// </summary>
        public string Phone { get; set; }
    }
}
