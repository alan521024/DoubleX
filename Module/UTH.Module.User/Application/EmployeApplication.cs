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
    /// 人员应用服务
    /// </summary>
    public class EmployeApplication :
        ApplicationCrudService<IEmployeDomainService, EmployeEntity, EmployeDTO, EmployeEditInput>,
        IEmployeApplication
    {
        IAccountDomainService accountService;
        IDomainDefaultService<MemberEntity> memberService;

        public EmployeApplication(IEmployeDomainService _service, IAccountDomainService _accountService, IDomainDefaultService<MemberEntity> _memberService, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
            accountService = _accountService;
            memberService = _memberService;
        }

        #region override

        public override EmployeDTO Insert(EmployeEditInput input)
        {
            EmployeDTO dto = null;
            using (var unit = CurrentUnitOfWorkManager.Begin())
            {
                var account = accountService.Create(Guid.Empty, input.Account, input.Mobile, input.Email, null, input.Password, input.Organize, input.Code, false);
                account.CheckNull();

                input.Id = account.Id;
                input.Name = service.GetDefaultName(input.Name, account);

                dto = MapperToDto(service.Insert(MapperToEntity(input)));

                unit.Complete();
            }
            return dto;
        }

        public override int Delete(EmployeDTO input)
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

        protected override List<KeyValueModel> InputToSorting(QueryInput<EmployeDTO> input)
        {
            if (input.Sorting.IsEmpty())
            {
                input.Sorting = new List<KeyValueModel>() {
                    new KeyValueModel("No","asc")
                };
            }
            return base.InputToSorting(input);
        }

        protected override Expression<Func<EmployeEntity, bool>> InputToWhere(QueryInput<EmployeDTO> input)
        {
            if (input.IsNull())
            {
                return base.InputToWhere(input);
            }

            if (input.Query.IsNull())
            {
                return base.InputToWhere(input);
            }

            if (input.Query.Code.IsEmpty() && input.Query.Organize.IsEmpty())
            {
                return base.InputToWhere(input);
            }

            var where = ExpressHelper.Get<EmployeEntity>();

            if (Session.User.Type != EnumAccountType.管理员)
            {
                input.Query.Organize = !input.Query.Organize.IsEmpty() ? input.Query.Organize : "-1";
            }

            where = where.AndIF(!input.Query.Code.IsEmpty(), x => x.Code.Contains(input.Query.Code));
            where = where.AndIF(!input.Query.Organize.IsEmpty(), x => x.Organize == input.Query.Organize);

            return where.ToExpression();
        }

        #endregion

        public List<EmployeDTO> BatchAdd(EmployeEditInput input)
        {
            input.CheckNull();
            input.Organize.CheckEmpty();

            List<EmployeDTO> list = new List<EmployeDTO>();

            if (input.BatchStart > 0 && input.BatchEnd > 0 && input.BatchEnd >= input.BatchStart)
            {
                var startNo = input.Code;
                for (var i = input.BatchStart; i <= input.BatchEnd; i++)
                {
                    input.Code = $"{startNo}{i}";
                    list.Add(Insert(input));
                }
            }

            return list;
        }
    }
}
