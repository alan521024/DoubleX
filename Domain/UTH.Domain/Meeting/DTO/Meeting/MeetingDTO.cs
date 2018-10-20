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
    /// 会议基本信息(DTO)
    /// </summary>
    [Serializable]
    public class MeetingDTO : IKeys, IOutput
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
