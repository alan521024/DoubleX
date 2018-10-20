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
    /// 验证码校验输入接口
    /// </summary>
    public interface ICaptchaVerifyInput
    {
        /// <summary>
        /// 图片验证码
        /// </summary>
        string ImgCode { get; set; }

        /// <summary>
        /// 图片验证码标识
        /// </summary>
        string ImgCodeKey { get; set; }

        /// 短信验证码
        /// </summary>
        string Mobile { get; set; }

        /// <summary>
        /// <summary>
        /// 短信验证码标识
        /// </summary>
        string SmsCodeKey { get; set; }

        /// 短信验证码
        /// </summary>
        string SmsCode { get; set; }
    }
}
