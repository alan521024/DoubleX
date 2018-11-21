namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 应用服务基类实现
    /// </summary>
    public abstract class BaseService
    {
        public BaseService(IApplicationSession session = null, ICachingService caching = null)
        {
            Session = session;
            Caching = caching;
        }

        /// <summary>
        /// 缓存服务
        /// </summary>
        protected readonly ICachingService Caching;

        /// <summary>
        /// 访问会话
        /// </summary>
        protected readonly IApplicationSession Session;
    }
}