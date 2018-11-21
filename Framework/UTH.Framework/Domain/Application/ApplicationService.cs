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
    public abstract class ApplicationService : BaseService, IApplicationService
    {
        public ApplicationService(IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {
        }
    }
}