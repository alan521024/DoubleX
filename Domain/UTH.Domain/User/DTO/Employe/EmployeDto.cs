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
    /// 人员信息(DTO)
    /// </summary>
    [Serializable]
    public class EmployeDTO : IKeys, IOutput
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
        /// 所属组织
        /// </summary>
        public string Organize { get; set; }

        /// <summary>
        /// 人员编号
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

        /// <summary>
        /// 人员账号状态
        /// </summary>
        public EnumAccountStatus Status { get; set; }

        public bool IsChecked { get; set; } = false;
    }
}
