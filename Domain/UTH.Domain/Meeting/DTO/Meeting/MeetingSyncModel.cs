using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Domain
{
    /// <summary>
    /// 会议系统同步信息
    /// </summary>
    [Serializable]
    public class MeetingSyncModel
    {
        /// <summary>
        /// 会议Id
        /// </summary>
        public Guid MeetingId { get; set; }

        /// <summary>
        /// 本地Key
        /// </summary>
        public Guid LocalId { get; set; }

        /// <summary>
        /// 会议记录Id
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// 翻译信息Id
        /// </summary>
        public Guid TranslationId { get; set; }

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
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 最后数据时间
        /// </summary>
        public DateTime LastDt { get; set; }
        
        /// <summary>
        /// 刷新时间(拼接为句子)
        /// </summary>
        public DateTime RefreshDt { get; set; }

        /// <summary>
        /// 同步提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 同步类型
        /// -1、0、本地识别记录(-1，句子未完成，0完成)
        /// 1: 会议记录
        /// 2：翻译数据
        /// </summary>
        public int SyncType { get; set; }

    }
}
