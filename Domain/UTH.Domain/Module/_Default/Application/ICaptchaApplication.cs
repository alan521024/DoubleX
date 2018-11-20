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
    /// 验证码应用服务接口
    /// </summary>
    public interface ICaptchaApplication : IApplicationService
    {
        /// <summary>
        /// 验证码发送
        /// </summary>
        CaptchaOutput Send(CaptchaInput input);

        /// <summary>
        /// 验证码校验
        /// </summary>
        bool Verify(CaptchaInput input);
    }
}
