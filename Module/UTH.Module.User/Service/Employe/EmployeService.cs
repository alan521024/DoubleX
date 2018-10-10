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
    /// 人员业务
    /// </summary>
    public class EmployeService : ApplicationDefault<EmployeEntity, EmployeEditInput, EmployeOutput>, IEmployeService
    {
        #region 构造函数

        IAccountService accountService;

        public EmployeService(IRepository<EmployeEntity> _repository, IAccountService _accountService) : base(_repository)
        {
            accountService = _accountService;
        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作

        #endregion

        #region 回调事件

        #endregion

        public override EmployeOutput Insert(EmployeEditInput input)
        {
            Session.CheckAccountOrganize(input.Organize);

            var isExist = Query(predicate: x => x.Organize == input.Organize && x.No == input.No);
            if (isExist.Count() > 0)
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHuYiCunZai);
            }

            var accountServer = DomainHelper.CreateTransactionService<IAccountService, AccountEntity>(repository);


            return base.Insert(input);
        }
    }
}
