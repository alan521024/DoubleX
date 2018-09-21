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
    /// 会议账号配置信息
    /// </summary>
    [SugarTable("MET_Profile")]
    public class MeetingProfileEntity : BaseGeneralEntity
    {
        public MeetingProfileEntity()
        {
        }

        /// <summary>
        /// 所属账号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 源语言
        /// </summary>
        public string SourceLang { get; set; }

        /// <summary>
        /// 目标语言
        /// </summary>
        public string TargetLangs { get; set; }

        /// <summary>
        /// 语速
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
    }
}
