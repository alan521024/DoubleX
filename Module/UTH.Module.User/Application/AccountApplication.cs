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
    /// 账户应用服务
    /// </summary>
    public class AccountApplication :
        ApplicationCrudService<IAccountDomainService, AccountEntity, AccountDTO, AccountEditInput>,
        IAccountApplication
    {
        IDomainDefaultService<MemberEntity> memberService;
        IDomainDefaultService<OrganizeEntity> organizeService;
        ITokenService tokenService;
        IUnitOfWorkManager unitMgr;

        public AccountApplication(IAccountDomainService _service, IDomainDefaultService<MemberEntity> _memberService, IDomainDefaultService<OrganizeEntity> _organizeService, ITokenService _tokenService, IApplicationSession session, ICachingService caching, IUnitOfWorkManager _unitMgr) :
            base(_service, session, caching)
        {
            memberService = _memberService;
            organizeService = _organizeService;
            tokenService = _tokenService;
            unitMgr = _unitMgr;
        }

        /// <summary>
        /// 账户检查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool CheckName(AccountEditInput input)
        {
            return !service.Get(account: input.Account, mobile: input.Mobile, email: input.Email, no: input.No).IsNull();
        }

        /// <summary>
        /// 账户注册
        /// </summary>
        [Captcha(EnumCaptchaCategory.Regist, EnumCaptchaMode.Image, EnumOperate.校验, EnumOperate.删除)]
        [Captcha(EnumCaptchaCategory.Regist, EnumCaptchaMode.Sms, EnumOperate.校验, EnumOperate.删除)]
        //[Notify(EnumNotificationCategory.Regist, EnumNotificationType.Sms)]
        public virtual RegistOutput Regist(RegistInput input)
        {
            input.CheckNull();
            TrimRegistInputSpace(input);

            using (var unit = unitMgr.Begin())
            {
                var account = service.Create(Guid.Empty, input.Account, input.Mobile, input.Email, null, input.Password, input.Organize);

                memberService.Insert(new MemberEntity() { Id = account.Id, Nickname = account.Account });

                if (!account.OrganizeNo.IsEmpty())
                {
                    organizeService.Insert(new OrganizeEntity() { Id = account.Id, No = account.OrganizeNo, Name = "" });
                }

                unit.SaveChanges();

                return EngineHelper.Map<RegistOutput>(account);
            }
        }

        /// <summary>
        /// 账户签入
        /// </summary>
        public virtual SignInOutput SignIn(SignInInput input)
        {
            input.CheckNull();
            TrimSinginInputSpace(input);

            var account = service.Login(input.Account, input.Mobile, input.Email, input.UserName, input.Password, null);

            var result = EngineHelper.Map<SignInOutput>(account);
            result.Token = tokenService.Generate(Session.Accessor.AppCode, account.Id, account.Account, account.Mobile, account.Email, account.RealName, "", account.OrganizeNo, account.EmployeNo, account.Type, account.Status);
            return result;
        }

        /// <summary>
        /// 账户签出
        /// </summary>
        public virtual bool SignOut(SignOutInput input)
        {
            return tokenService.Remove(input.Token);
        }

        /// <summary>
        /// Token刷新
        /// </summary>
        public virtual string Refresh(SignRefreshInput input)
        {
            input.Token.CheckEmpty();
            return tokenService.Refresh(input.Token);
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool FindPwd(FindPwdInput input)
        {
            input.CheckNull();
            input.Password.CheckEmpty();

            var account = service.Get(null, input.Mobile, input.Email, string.Empty);
            if (account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHaoCuoWu);
            }

            account.Password = HashSecurityHelper.GetSecurity(input.Password);

            return !service.Update(account).IsNull();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool EditPwd(EditPwdInput input)
        {
            input.CheckNull();

            var account = service.Get(input.AccountId);
            if (account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHaoCuoWu);
            }

            if (!HashSecurityHelper.VerifySecurity(account.Password, input.OldPassword))
            {
                throw new DbxException(EnumCode.提示消息, Lang.userQingShuRuYouXiaoDeYuanMiMa);
            }

            account.Password = HashSecurityHelper.GetSecurity(input.NewPassword);

            return !service.Update(account).IsNull();
        }



        /// <summary>
        /// 登录移除关键项前后空格
        /// </summary>
        private void TrimSinginInputSpace(SignInInput input)
        {

            if (input.IsNull())
                return;

            if (!input.UserName.IsEmpty())
            {
                input.UserName = input.UserName.Trim();
            }
            if (!input.Account.IsEmpty())
            {
                input.Account = input.Account.Trim();
            }
            if (!input.Mobile.IsEmpty())
            {
                input.Mobile = input.Mobile.Trim();
            }
            if (!input.Email.IsEmpty())
            {
                input.Email = input.Email.Trim();
            }
            if (!input.Password.IsEmpty())
            {
                input.Password = input.Password.Trim();
            }
        }

        /// <summary>
        /// 注册移除关键项前后空格
        /// </summary>
        private void TrimRegistInputSpace(RegistInput input)
        {
            if (input.IsNull())
                return;

            if (!input.Account.IsEmpty())
            {
                input.Account = input.Account.Trim();
            }
            if (!input.Mobile.IsEmpty())
            {
                input.Mobile = input.Mobile.Trim();
            }
            if (!input.Email.IsEmpty())
            {
                input.Email = input.Email.Trim();
            }
            if (!input.Password.IsEmpty())
            {
                input.Password = input.Password.Trim();
            }
            if (!input.Organize.IsEmpty())
            {
                input.Organize = input.Organize.ToLower();
            }
        }
    }
}
