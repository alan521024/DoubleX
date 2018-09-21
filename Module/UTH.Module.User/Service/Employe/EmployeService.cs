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
    /// 组员业务
    /// </summary>
    public class EmployeService : ApplicationDefault<EmployeEntity, EmployeEditInput, EmployeOutput>, IEmployeService
    {
        public EmployeService(IRepository<EmployeEntity> _repository) : base(_repository) { }
    }
}
