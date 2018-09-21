namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 验证码业务接口
    /// </summary>
    public interface ICaptchaService : IApplicationService
    {
        /// <summary>
        /// 验证码发送/获取
        /// </summary>
        CaptchaOutput Send(CaptchaSendInput input);

        /// <summary>
        /// 验证码校验
        /// </summary>
        bool Verify(CaptchaVerifyInput input);

        /// <summary>
        /// 验证码移除
        /// </summary>
        bool Remove(CaptchaVerifyInput input);
    }
}
