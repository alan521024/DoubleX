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
    /// 组织用户领域服务
    /// </summary>
    public class OrganizeDomainService : DomainDefaultService<IOrganizeRepository, OrganizeEntity>, IOrganizeDomainService
    {
        /// <summary>
        /// 账号仓储
        /// </summary>
        IAccountRepository accountRepository;

        public OrganizeDomainService(IOrganizeRepository repository, IAccountRepository _accountRepository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
            accountRepository = _accountRepository;
        }

        #region override

        protected override List<OrganizeEntity> InsertBefore(List<OrganizeEntity> inputs)
        {
            var codes = inputs.Select(x => x.Code.ToUpper()).ToList();
            var names = inputs.Select(x => x.Name.ToUpper()).ToList();

            var isExist = Any(x => codes.Contains(x.Code.ToUpper()) || names.Contains(x.Name.ToUpper()));
            if (isExist)
            {
                throw new DbxException(EnumCode.提示消息, Lang.sysBianHaoHuoMingChengYiCunZai);
            }

            return base.InsertBefore(inputs);
        }

        protected override List<OrganizeEntity> UpdateBefore(List<OrganizeEntity> inputs)
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
                entity.Fax = input.Fax;
                entity.AreaCode = input.AreaCode;
                entity.Address = input.Address;
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
                    !account.OrganizeCode.IsEmpty() ? account.OrganizeCode :
                    !account.Mobile.IsEmpty() ? account.Mobile :
                    !account.Email.IsEmpty() ? account.Email :
                    !account.Account.IsEmpty() ? account.Account : "";
        }
    }
}
