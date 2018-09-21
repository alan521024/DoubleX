using System.Collections.Generic;
using System.Security.Claims;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using System.Threading;

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// WPF访问者
    /// </summary>
    public class WpfAccessor : DefaultAccessor
    {
        public override Dictionary<string, string> Items
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
