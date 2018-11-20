namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 同步操作输入参数
    /// </summary>
    public class MeetingSyncInput : IInput
    {
        /// <summary>
        /// Ids
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 读取数量
        /// </summary>
        public int Top { get; set; } = 10;

        /// <summary>
        /// 所属会议
        /// </summary>
        public Guid MeetingId { get; set; }

        /// <summary>
        /// 方向（向前/pre:加载历义记录,向后/next:加载最新记录）
        /// </summary>
        public EnumDirection Direction { get; set; } = EnumDirection.Next;

        /// <summary>
        /// 会议记录同步时间
        /// </summary>
        public DateTime RecordDt { get; set; }

        /// <summary>
        /// 翻译记录同步时间
        /// </summary>
        public DateTime TranslationDt { get; set; }
    }
}

