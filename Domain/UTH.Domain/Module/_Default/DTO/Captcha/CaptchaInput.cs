namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 验证码信息基类
    /// </summary>
    public class CaptchaInput : IInput
    {
        /// <summary>
        /// 验证码类别
        /// </summary>
        public EnumCaptchaCategory Category { get; set; }

        /// <summary>
        /// 验证方式
        /// </summary>
        public EnumCaptchaMode Mode { get; set; } = EnumCaptchaMode.Default;

        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; } = 4;
    }
}
