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
    /// 会议记录信息
    /// </summary>
    [Serializable]
    public class MeetingRecordBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 会议Id
        /// </summary>
        public Guid MeetingId { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Langue { get; set; }

        /// <summary>
        /// 翻译语言以'|'分割
        /// </summary>
        public string LangueTrs { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 本地Id
        /// </summary>
        public Guid LocalId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
