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
    /// 通知方式
    /// </summary>
    public enum EnumNotificationType
    {
        Default,
        Image,
        Sms,
        Email,
        Voice,
    }
}
