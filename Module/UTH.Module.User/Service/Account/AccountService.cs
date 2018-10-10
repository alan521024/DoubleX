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
    /// 账户业务 
    /// </summary>
    public class AccountService : ApplicationDefault<IAccountRepository, AccountEntity, AccountEditInput, AccountOutput>, IAccountService
    {
        #region 构造函数

        IEmployeService employeService;
        ITokenService tokenService;

        public AccountService(IAccountRepository _repository, IEmployeService _employeService, ITokenService _tokenService) : base(_repository)
        {
            employeService = _employeService;
            tokenService = _tokenService;
        }

        #endregion

        #region 公共属性

        #endregion

        #region 私有变量

        #endregion

        #region 辅助操作

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
        }

        #endregion

        #region 重写操作

        public override Action<AccountEditInput> InsertBeforeCall => (input) =>
        {
            var maxNum = repository.Max<string>(field: x => x.No, predicate: x => x.Type == input.Type);
            if (maxNum.IsEmpty())
            {
                input.No = $"{input.Type}00000000";
            }
            else
            {
                input.No = StringHelper.Get(IntHelper.Get(maxNum) + 1);
            }
        };
        public override Func<AccountOutput, AccountOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<AccountEditInput, AccountEntity, AccountEntity> UpdateBeforeCall => (input, entity) =>
        {
            //entity.Name = input.Name;
            //entity.AppType = input.AppType;
            return entity;
        };
        public override Func<AccountOutput, AccountOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<AccountEntity, bool>> FindWhere(QueryInput param)
        {
            var exp = ExpressHelper.Get<AccountEntity>();

            #region Input

            if (!param.IsNull() && !param.Query.IsNull())
            {
                string key = param.Query.GetString("key"), name = param.Query.GetString("name");
                int appType = param.Query.GetInt("appType");

                //exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                //exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                //exp = exp.AndIF(!appType.IsEmpty(), x => x.AppType == appType);
            }

            #endregion

            return exp.ToExpression();
        }

        #endregion
        
        /// <summary>
        /// 账户签入
        /// </summary>
        public virtual SignInOutput SignIn(SignInInput input)
        {
            //信息校验
            input.CheckNull();
            if (input.UserName.IsEmpty() && (input.Account + input.Mobile + input.Email).IsEmpty())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userQingShuRuZhangHaoShouJiHaoMaYouXiangDiZhi);
            }

            //移除空格
            TrimSinginInputSpace(input);

            //账号登录
            var account = FindAccount(input.Account, input.Mobile, input.Email, input.UserName);
            if (account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHaoCuoWu);
            }
            if (!HashSecurityHelper.VerifySecurity(account.Password, input.Password))
            {
                throw new DbxException(EnumCode.提示消息, Lang.userMiMaCuoWu);
            }

            account.LoginCount++;
            account.LoginLastDt = DateTime.Now;
            account.LoginLastIp = Session.Accessor.ClientIp;

            repository.Update(account);

            var mebService = DomainHelper.CreateTransactionService<IMemberService, MemberEntity>(repository);
            var orgService = DomainHelper.CreateTransactionService<IOrganizeService, OrganizeEntity>(repository);
            var empService = DomainHelper.CreateTransactionService<IEmployeService, EmployeEntity>(repository);

            var type = EnumsHelper.Get<EnumAccountType>(account.Type);
            var status = EnumsHelper.Get<EnumAccountStatus>(account.Status);
            string organize = string.Empty, employe = string.Empty;
            if (type == EnumAccountType.组织)
            {
                var org = orgService.Get(account.Id);
                organize = org?.No;
            }
            if (type == EnumAccountType.人员)
            {
                var emp = empService.Get(account.Id);
                employe = emp?.No;
            }

            var result = EngineHelper.Map<SignInOutput>(account);

            result.Token = tokenService.Generate(Session.Accessor.AppCode, account.Id, account.Account, account.Mobile, account.Email, account.RealName, "", organize, employe, type, status);

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
        public virtual string SignRefresh(SignRefreshInput input)
        {
            input.Token.CheckEmpty();
            return tokenService.Refresh(input.Token);
        }

        /// <summary>
        /// 账户注册
        /// </summary>
        [CaptchaVerify(EnumNotificationCategory.Regist, EnumNotificationType.Image, EnumCaptchaOperate.Verify, EnumCaptchaOperate.Remove)]
        [CaptchaVerify(EnumNotificationCategory.Regist, EnumNotificationType.Sms, EnumCaptchaOperate.Verify, EnumCaptchaOperate.Remove)]
        //[Notification(EnumNotificationCategory.Regist, EnumNotificationType.Sms)]
        public virtual RegistOutput Regist(RegistInput input)
        {
            input.CheckNull();
            TrimRegistInputSpace(input);

            var mebService = DomainHelper.CreateTransactionService<IMemberService, MemberEntity>(repository);
            var orgService = DomainHelper.CreateTransactionService<IOrganizeService, OrganizeEntity>(repository);
            var empService = DomainHelper.CreateTransactionService<IEmployeService, EmployeEntity>(repository);

            //输入校验
            if ((input.Account + input.Mobile + input.Email).IsEmpty())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userQingShuRuZhangHaoShouJiHaoMaYouXiangDiZhi);
            }

            var organize = input.Organize?.ToLower();
            if (!organize.IsEmpty() && orgService.Query(where: x => x.No == organize).Count() > 0)
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHuYiCunZai);
            }

            //账号信息
            var queryAct = FindAccount(input.Account, input.Mobile, input.Email, string.Empty);
            if (!queryAct.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHuYiCunZai);
            }

            var actInput = new AccountEditInput();

            actInput.Id = Guid.NewGuid();
            actInput.Account = !input.Account.IsEmpty() ? input.Account : string.Format("AUTO_{0}", GuidHelper.GetToString(actInput.Id, removeSplit: true, isCaseUpper: true));
            actInput.Mobile = input.Mobile;
            actInput.Email = input.Email;

            actInput.Password = HashSecurityHelper.GetSecurity(input.Password);

            actInput.MobileAuth = false;
            actInput.EmailAuth = false;
            actInput.CertificateAuth = false;

            //DO:使用手机号码，邮箱地址注册，需进行校验码验证，才能进入该方法，考虑使用AOP(CODE+接收者+类型)对Input输入继承ICodeCheckService 进行校验
            if (!actInput.Mobile.IsEmpty())
            {
                actInput.MobileAuth = true;
            }
            if (!actInput.Email.IsEmpty())
            {
                actInput.EmailAuth = true;
            }

            actInput.NormalizedAccount = !actInput.Account.IsEmpty() ? actInput.Account.ToUpper() : "";
            actInput.NormalizedEmail = !actInput.Email.IsEmpty() ? actInput.Email.ToUpper() : "";

            actInput.IsTwoFactorEnabled = false;
            actInput.IsLockoutEnabled = false;
            actInput.AccessFailedCount = 0;
            actInput.InviterId = Guid.Empty;

            actInput.LoginCount = 0;
            actInput.LoginLastIp = "127.0.0.1";
            actInput.LoginLastDt = DateTimeHelper.DefaultDateTime;

            actInput.Type = EnumAccountType.个人.GetValue();
            actInput.Status = EnumAccountStatus.正常.GetValue();

            //成员信息
            var mebInput = new MemberEntity();
            mebInput.Id = actInput.Id;
            mebInput.Nickname = actInput.Account;
            mebInput.Gender = EnumGender.男.GetValue();

            //组织信息
            OrganizeEditInput orgInput = null;
            if (!organize.IsEmpty())
            {
                actInput.Type = EnumAccountType.组织.GetValue();
                orgInput = new OrganizeEditInput();
                orgInput.Id = actInput.Id;
                orgInput.No = organize;
                orgInput.Name = "";
            }

            var result = repository.UseTransaction((obj) =>
            {
                Insert(actInput);

                mebService.Insert(mebInput);

                if (orgInput != null)
                {
                    orgService.Insert(orgInput);
                }
            });
            if (!result)
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhuCeShiBai);
            }

            return EngineHelper.Map<RegistOutput>(actInput);
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

            var account = FindAccount(null, input.Mobile, input.Email, string.Empty);
            if (account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHaoCuoWu);
            }

            account.Password = HashSecurityHelper.GetSecurity(input.Password);

            return repository.Update(account) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool EditPwd(EditPwdInput input)
        {
            input.CheckNull();

            var account = repository.Find(input.AccountId);
            if (account.IsNull())
            {
                throw new DbxException(EnumCode.提示消息, Lang.userZhangHaoCuoWu);
            }

            if (!HashSecurityHelper.VerifySecurity(account.Password, input.OldPassword))
            {
                throw new DbxException(EnumCode.提示消息, Lang.userQingShuRuYouXiaoDeYuanMiMa);
            }

            account.Password = HashSecurityHelper.GetSecurity(input.NewPassword);
            return repository.Update(account) > 0;
        }

        /// <summary>
        /// 账户查找
        /// (account/mobile/email-[userName])
        /// </summary>
        public virtual AccountEntity FindAccount(string account = null, string mobile = null, string email = null, string userName = null)
        {
            if (userName.IsEmpty() && (account + email + mobile).IsEmpty())
            {
                return null;
            }

            AccountEntity entity = null;

            if (entity.IsNull() && (!userName.IsEmpty() || !account.IsEmpty()))
            {
                var value = (!account.IsEmpty() ? account : userName).ToUpper();
                entity = repository.Find(x => x.NormalizedAccount == value);
            }

            if (entity.IsNull() && (!userName.IsEmpty() || !mobile.IsEmpty()))
            {
                var value = (!mobile.IsEmpty() ? mobile : userName).ToUpper();
                entity = repository.Find(x => x.Mobile == value && x.MobileAuth);
            }

            if (entity.IsNull() && (!userName.IsEmpty() || !email.IsEmpty()))
            {
                var value = (!email.IsEmpty() ? email : userName).ToUpper();
                entity = repository.Find(x => x.NormalizedEmail == value && x.EmailAuth);
            }

            return entity;
        }

    }
}
