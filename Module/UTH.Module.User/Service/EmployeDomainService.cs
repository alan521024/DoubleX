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

        protected override List<EmployeEntity> InsertBefore(List<EmployeEntity> inputs)
        {
            var organizes = inputs.Select(x => x.Organize).GroupBy(x => x);
            foreach (var org in organizes)
            {
                var codes = inputs.Where(x => x.Organize == org.Key).Select(x => x.Code.ToUpper()).ToList();
                var isExist = Any(x => x.Organize == org.Key && codes.Contains(x.Code.ToUpper()));
                if (isExist)
                {
                    throw new DbxException(EnumCode.提示消息, Lang.sysBianHaoYiCunZai);
                }
            }
            return base.InsertBefore(inputs);
        }

        protected override List<EmployeEntity> UpdateBefore(List<EmployeEntity> inputs)
        {
            if (inputs.IsEmpty())
                return inputs;

            var ids = inputs.Select(x => x.Id).ToList();
            var entitys = Find(where: x => ids.Contains(x.Id));

            foreach (var entity in entitys)
            {
                var input = inputs.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (input.IsNull())
                {
                    continue;
                }
                //entity.Code = input.Code;
                entity.Name = input.Name;
                entity.Phone = input.Phone;
            }

            return entitys;
        }

        #endregion

        /// <summary>
        /// 获取默认名称
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetDefaultName(string inputName, AccountEntity account)
        {
            account.CheckNull();

            return !inputName.IsEmpty() ? inputName :
                    !account.EmployeCode.IsEmpty() ? account.EmployeCode :
                    !account.Mobile.IsEmpty() ? account.Mobile :
                    !account.Email.IsEmpty() ? account.Email :
                    !account.Account.IsEmpty() ? account.Account : "";
        }
    }
}
