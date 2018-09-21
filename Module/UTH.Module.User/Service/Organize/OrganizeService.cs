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
    /// 组织业务
    /// </summary>
    public class OrganizeService : ApplicationDefault<OrganizeEntity, OrganizeEditInput, OrganizeOutput>, IOrganizeService
    {
        public OrganizeService(IRepository<OrganizeEntity> _repository) : base(_repository) { }
    }
}
