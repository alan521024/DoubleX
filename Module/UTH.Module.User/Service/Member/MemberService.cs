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
    /// 会员业务
    /// </summary>
    public class MemberService : ApplicationDefault<MemberEntity>, IMemberService
    {
        public MemberService(IRepository<MemberEntity> _repository) : base(_repository) { }

    }
}
