namespace UTH.Module.User
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
    /// 会员应用服务
    /// </summary>
    public class MemberApplication :
        ApplicationCrudService<MemberEntity, MemberDTO, MemberEditInput>,
        IMemberApplication
    {
        public MemberApplication(IDomainDefaultService<MemberEntity> _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }
    }
}
