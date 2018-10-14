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
    /// 会员信息
    /// </summary>
    [SugarTable("UC_Member")]
    public class MemberEntity : BaseFullEntity
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthdate { get; set; }
    }
}
