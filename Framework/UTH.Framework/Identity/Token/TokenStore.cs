namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    ///Token存储服务
    /// </summary>
    public class TokenStore : RedisCachingService, ITokenStore
    {
        public TokenStore(ConnectionModel model) : base(model)
        {
        }
    }
}
