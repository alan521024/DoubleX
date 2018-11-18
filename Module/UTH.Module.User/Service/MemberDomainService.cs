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
    /// 个人用户领域服务
    /// </summary>
    public class MemberDomainService : DomainDefaultService<IMemberRepository, MemberEntity>, IMemberDomainService
    {
        IAccountRepository accountRepository;

        public MemberDomainService(IMemberRepository repository, IAccountRepository _accountRepository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
            accountRepository = _accountRepository;
        }

        #region override

        protected override List<MemberEntity> UpdateBefore(List<MemberEntity> inputs)
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

                entity.Name = input.Name;
                entity.Gender = input.Gender;
                entity.Birthdate = input.Birthdate;
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
                    !account.Mobile.IsEmpty() ? account.Mobile :
                    !account.Email.IsEmpty() ? account.Email :
                    !account.Account.IsEmpty() ? account.Account : "";
        }
    }
}
