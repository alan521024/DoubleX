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
    /// 人员领域服务
    /// </summary>
    public class EmployeDomainService : DomainDefaultService<IEmployeRepository, EmployeEntity>, IEmployeDomainService
    {
        IAccountRepository accountRepository;
        public EmployeDomainService(IEmployeRepository repository, IAccountRepository _accountRepository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
            accountRepository = _accountRepository;
        }

        #region override

        public override int Delete(Guid id)
        {
            accountRepository.Delete(id);
            return base.Delete(id);
        }

        public override int Delete(List<Guid> ids)
        {
            accountRepository.Delete(ids);
            return base.Delete(ids);
        }

        #endregion
    }
}
