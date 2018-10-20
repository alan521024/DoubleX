namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Xml.Serialization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 短信发送结果
    /// </summary>
    public class SmsSendOutput : IOutput
    {
        /// <summary>
        /// 成功标识
        /// </summary>
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
