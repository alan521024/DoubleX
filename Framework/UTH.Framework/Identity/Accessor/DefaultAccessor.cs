namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 默认访问器
    /// </summary>
    public class DefaultAccessor : IAccessor, ISingletonDependency
    {
        /// <summary>
        /// 访问者(当事人信息)
        /// </summary>
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;

        /// <summary>
        /// 访问数据项
        /// </summary>
        public virtual Dictionary<string, string> Items
        {
            get
            {
                var _items = new Dictionary<string, string>();
                _items.Add("Culture", Thread.CurrentThread.CurrentCulture.Name);
                _items.Add("AppCode", EngineHelper.Configuration.AppCode);
                _items.Add("ClientIp", "127.0.0.1");
                return _items;
            }
        }

    }
}