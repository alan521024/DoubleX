namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 账户业务接口
    /// </summary>
    public interface IAccountService : IApplicationDefault<IAccountRepository, AccountEntity, AccountEditInput, AccountOutput>
    {
        /// <summary>
        /// 账户签入
        /// </summary>
        /// <param name="input">SignInInput</param>
        /// <returns>SignInOutput</returns>
        SignInOutput SignIn(SignInInput input);

        /// <summary>
        /// 账户签出
        /// </summary>
        /// <param name="input">SignOutInput</param>
        /// <returns>bool</returns>
        bool SignOut(SignOutInput input);

        /// <summary>
        /// Token刷新
        /// </summary>
        string SignRefresh(SignRefreshInput input);

        /// <summary>
        /// 账户注册
        /// </summary>
        /// <param name="input">RegistInput</param>
        /// <returns>RegistOutput</returns>
        RegistOutput Regist(RegistInput input);

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool FindPwd(FindPwdInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool EditPwd(EditPwdInput input);

        /// <summary>
        /// 账户查找(account/mobile/email-[userName])
        /// </summary>

        AccountEntity FindAccount(string account = null, string mobile = null, string email = null, string userName = null);


        /// <summary>
        /// 账户创建
        /// </summary>
        /// <param name="input">RegistInput</param>
        /// <param name="isInnerTransaction"></param>
        /// <returns></returns>
        AccountEntity Create(RegistInput input, bool isInnerTransaction = true);
    }
}
