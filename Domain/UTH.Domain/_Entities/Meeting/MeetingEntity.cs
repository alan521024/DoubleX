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
    /// 会议信息
    /// </summary>
    [SugarTable("MET_Meeting")]
    public class MeetingEntity : BaseGeneralEntity
    {
        public MeetingEntity()
        {
        }

        /// <summary>
        /// 会议编号
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 会议名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 会议描述
        /// </summary>
        public string Descript { get; set; }

        /// <summary>
        /// 会议配置JSON
        /// </summary>
        public string Setting { get; set; }
    }
}
