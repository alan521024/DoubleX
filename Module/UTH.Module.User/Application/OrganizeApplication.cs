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
    /// 组织应用服务
    /// </summary>
    public class OrganizeApplication :
        ApplicationCrudService<OrganizeEntity, OrganizeDTO, OrganizeEditInput>,
        IOrganizeApplication
    {
        IAccountDomainService accountService;

        public OrganizeApplication(IOrganizeDomainService _service, IAccountDomainService _accountService, IApplicationSession session, ICachingService caching, IUnitOfWorkManager _unitMgr) :
            base(_service, session, caching)
        {
            accountService = _accountService;
        }

        #region override

        public override OrganizeDTO Insert(OrganizeEditInput input)
        {
            OrganizeDTO dto = null;
            //using (var unit = unitManager.Begin())
            //{
            //    var _actService = unit.Resolve<AccountEntity, IAccountDomainService, IAccountRepository>();
            //    var _orgService = unit.Resolve<OrganizeEntity, IOrganizeDomainService, IOrganizeRepository>();

            //    var account = accountService.Create(Guid.Empty, input.Account, input.Mobile, input.Email, null, input.Password, input.Code);
            //    account.CheckNull();

            //    input.Id = account.Id;
            //    input.Name = input.Name ?? input.Code ?? account.Account;

            //    dto = MapperToDto(_orgService.Insert(MapperToEntity(input)));

            //    unit.SaveChanges();
            //}
            return dto;
        }

        public override int Delete(OrganizeDTO input)
        {
            int rows = 0;
            //using (var unit = unitManager.Begin())
            //{
            //    if (!input.Ids.IsEmpty())
            //    {
            //        rows += accountService.Delete(input.Ids);
            //    }
            //    if (!input.Id.IsEmpty())
            //    {
            //        rows += accountService.Delete(input.Id);
            //    }

            //    rows += base.Delete(input);
            //}
            return rows;
        }

        #endregion
    }
}
