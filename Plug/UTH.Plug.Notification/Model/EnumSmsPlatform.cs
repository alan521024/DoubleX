namespace UTH.Plug.Notification
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
    /// 短信平台列表
    /// </summary>
    public enum EnumSmsPlatform
    {
        Default,
        LuoSiMao,
        Aliyun,
    }
}
