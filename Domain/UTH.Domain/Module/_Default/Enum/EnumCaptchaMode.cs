namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 验证方式
    /// </summary>
    public enum EnumCaptchaMode
    {
        Default,
        /// <summary>
        /// 验证编码
        /// </summary>
        Text,
        /// <summary>
        /// 验证图片
        /// </summary>
        Image,
        /// <summary>
        /// 短信验证
        /// </summary>
        Sms,
        /// <summary>
        /// 邮件验证
        /// </summary>
        Email,
        /// <summary>
        /// 语音验证
        /// </summary>
        Voice
    }
}
