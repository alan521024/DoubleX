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
    /// 个人用户应用服务
    /// </summary>
    public class MemberApplication :
        ApplicationCrudService<IMemberDomainService, MemberEntity, MemberDTO, MemberEditInput>,
        IMemberApplication
    {
        IAccountDomainService accountService;
        IUnitOfWorkManager unitMgr;

        public MemberApplication(IMemberDomainService _service, IAccountDomainService _accountService, IApplicationSession session, ICachingService caching, IUnitOfWorkManager _unitMgr) :
            base(_service, session, caching)
        {
            accountService = _accountService;
            unitMgr = _unitMgr;
        }

        #region override

        public override MemberDTO Insert(MemberEditInput input)
        {
            MemberDTO dto = null;
            using (var unit = unitMgr.Begin())
            {
                //var account = accountService.Create(Guid.Empty, input.Account, input.Mobile, input.Email, null, input.Password, null);
                //input.Id = account.Id;
                //input.Name = input.Name ?? account.Account;
                //dto = base.Insert(input);
                //unit.SaveChanges();
            }
            return dto;
        }

        public override int Delete(MemberDTO input)
        {
            int rows = 0;
            //using (var unit = unitMgr.Begin())
            //{
            //    if (!input.Ids.IsEmpty())
            //    {
            //        rows += accountService.Delete(input.Ids);
            //    }
            //    if (!input.Id.IsEmpty())
            //    {
            //        rows += accountService.Delete(input.Id);
            //    }
            //}
            return base.Delete(input);
        }

        #endregion

    }
}
