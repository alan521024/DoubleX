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
    /// 个人用户信息(DTO)
    /// </summary>
    [Serializable]
    public class MemberDTO : AccountDTO, IKeys, IOutput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别(EnumGender)
        /// </summary>
        public EnumGender Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderText { get { return Gender.GetName(); } }
    }
}
