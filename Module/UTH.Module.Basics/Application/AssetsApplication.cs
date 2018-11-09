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

    /// <summary>
    /// 资源信息应用服务
    /// </summary>
    public class AssetsApplication :
        ApplicationCrudService<AssetsEntity, AssetsDTO, AssetsEditInput>,
        IAssetsApplication
    {
        public AssetsApplication(IDomainDefaultService<AssetsEntity> _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }
    }
}
