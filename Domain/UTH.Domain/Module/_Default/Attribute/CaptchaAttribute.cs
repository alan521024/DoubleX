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
    /// 验证码校验 属性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CaptchaAttribute : Attribute
    {
        public CaptchaAttribute() { }

        public CaptchaAttribute(EnumCaptchaCategory category, EnumCaptchaMode mode, params EnumOperate[] operates)
        {
            Category = category;
            Mode = mode;
            Operates = !operates.IsEmpty() ? operates.ToList() : new List<EnumOperate>();
        }

        /// <summary>
        /// 验证码类别
        /// </summary>
        public EnumCaptchaCategory Category { get; set; }

        /// <summary>
        /// 验证方式
        /// </summary>
        public EnumCaptchaMode Mode { get; set; }
        
        /// <summary>
        /// 操作列表
        /// </summary>
        public List<EnumOperate> Operates { get; set; } = new List<EnumOperate>();

        /// <summary>
        /// 判断input tag 及 code 为 空 '' 时，是否校验
        /// </summary>
        public bool IsMust { get; set; } = false;

        /// <summary>
        /// 自定义内容
        /// </summary>
        public string Content { get; set; }

    }
}
