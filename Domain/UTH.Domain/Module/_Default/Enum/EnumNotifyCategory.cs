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

    /// <summary>
    /// 通知分类
    /// </summary>
    public enum EnumNotifyCategory
    {
        Default,
        注册验证,
        注册结果,
        登录校验,
        登录提示,
        找回密码验证,
        找回密码结果
    }
}
