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

            if (input.Query.No.IsEmpty() && input.Query.Organize.IsEmpty())
            {
                return base.InputToWhere(input);
            }

            var where = ExpressHelper.Get<EmployeEntity>();

            if (Session.User.Type != EnumAccountType.管理员)
            {
                input.Query.Organize = input.Query.Organize ?? "-1";
            }

            where = where.AndIF(!input.Query.No.IsEmpty(), x => x.No.Contains(input.Query.No));
            where = where.AndIF(!input.Query.Organize.IsEmpty(), x => x.Organize == input.Query.Organize);

            return where.ToExpression();
        }

        #endregion

        public EmployeDTO Create(EmployeEditInput input)
        {
            input.CheckNull();
            input.Organize.CheckEmpty();

            if (input.BatchStart > 0 && input.BatchEnd > 0 && input.BatchEnd >= input.BatchStart)
            {
                var startNo = input.No;
                List<AccountEntity> accounts = new List<AccountEntity>();
                for (var i = input.BatchStart; i <= input.BatchEnd; i++)
                {
                    input.No = $"{startNo}{i}";
                    var item = Creates(input);
                    if (!item.IsNull())
                    {
                        accounts.Add(item);
                    }
                }

                EmployeDTO output = null;

                for (var j = 0; j < accounts.Count; j++)
                {
                    var account = accounts[j];

                    input.Id = Guid.NewGuid();
                    input.No = $"{startNo + (input.BatchStart + j)}";

                    account.Id = input.Id;

                    var mebInput = new MemberEntity();
                    mebInput.Id = input.Id;
                    mebInput.Name = input.Name;
                    mebInput.Gender = EnumGender.男;

                    output = Insert(input);
                    accountService.Create(account);
                    memberService.Insert(mebInput);
                }

                return output;
            }
            else
            {
                var account = Creates(input);
                if (account != null)
                {
                    input.Id = Guid.NewGuid();

                    account.Id = input.Id;

                    var mebInput = new MemberEntity();
                    mebInput.Id = input.Id;
                    mebInput.Name = input.Name;
                    mebInput.Gender = EnumGender.男;

                    var output = Insert(input);
                    accountService.Create(account);
                    memberService.Insert(mebInput);
                    return output;
                }
            }

            return null;
        }

        private AccountEntity Creates(EmployeEditInput input)
        {
            string name = $"{input.No}@{input.Organize}";
            var account = accountService.Get(account: name);

            if (!account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHuYiCunZai);
            }

            if (service.Count(x => x.No == input.No && x.Organize == input.Organize) > 0)
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHuYiCunZai);
            }

            account = new AccountEntity()
            {
                Account = name,
                Password = input.Password,
                Type = EnumAccountType.人员,
                Status = EnumAccountStatus.正常
            };

            return account;
        }
    }
}
