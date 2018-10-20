namespace UTH.Module.Basics
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
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 会议信息应用服务
    /// </summary>
    public class AppVersionApplication :
        ApplicationCrudService<AppVersionEntity, AppVersionDTO, AppVersionEditInput>,
        IAppVersionApplication
    {
        public AppVersionApplication(IDomainDefaultService<AppVersionEntity> _service, IApplicationSession session, ICachingService caching) : 
            base(_service, session, caching)
        {
        }
    }
}
