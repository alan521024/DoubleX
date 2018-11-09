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
    /// 个人用户信息
    /// </summary>
    [SugarTable("UC_Member")]
    public class MemberEntity : UserAccountExtendEntity
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
        /// 账号编号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string No { get; set; }
    }
}
