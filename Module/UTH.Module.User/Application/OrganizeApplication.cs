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
        ApplicationCrudService<IOrganizeDomainService, OrganizeEntity, OrganizeDTO, OrganizeEditInput>,
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
            using (var unit = CurrentUnitOfWorkManager.Begin())
            {
                var account = accountService.Create(Guid.Empty, input.Account, input.Mobile, input.Email, null, input.Password, input.Code, null,false);
                account.CheckNull();

                input.Id = account.Id;
                input.Name = service.GetDefaultName(input.Name, account);

                dto = MapperToDto(service.Insert(MapperToEntity(input)));

                unit.Complete();
            }
            return dto;
        }

        public override int Delete(OrganizeDTO input)
        {
            int rows = 0;
            using (var unit = CurrentUnitOfWorkManager.Begin())
            {
                if (!input.Ids.IsEmpty())
                {
                    rows += accountService.Delete(input.Ids);
                }
                if (!input.Id.IsEmpty())
                {
                    rows += accountService.Delete(input.Id);
                }
                rows += base.Delete(input);

                unit.Complete();
            }
            return rows;
        }

        protected override Expression<Func<OrganizeEntity, bool>> InputToWhere(QueryInput<OrganizeDTO> input)
        {
            if (input.IsNull())
            {
                return base.InputToWhere(input);
            }

            if (input.Query.IsNull())
            {
                return base.InputToWhere(input);
            }
            
            var where = ExpressHelper.Get<OrganizeEntity>();

            if (Session.User.Type != EnumAccountType.管理员)
            {
                //input.Query. = !input.Query.Organize.IsEmpty() ? input.Query.Organize : "-1";
            }

            where = where.AndIF(!input.Query.Code.IsEmpty(), x => x.Code.Contains(input.Query.Code));
            //where = where.AndIF(!input.Query.Organize.IsEmpty(), x => x.Organize == input.Query.Organize);

            return where.ToExpression();
        }

        #endregion
    }
}
