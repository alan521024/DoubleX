namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 账户校验
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AccountValidator<T> : AbstractValidator<T>
    {
        protected virtual Expression<Func<T, string>> accountExpression { get; }
        protected virtual Expression<Func<T, string>> mobileExpression { get; }
        protected virtual Expression<Func<T, string>> emailExpression { get; }
        protected virtual Expression<Func<T, string>> passwordExpression { get; }

        private IRuleBuilderInitial<T, string> accountRoule { get { return RuleFor<string>(accountExpression).Configure(x => { x.PropertyName = Lang.userZhangHao; }); } }
        private IRuleBuilderInitial<T, string> mobileRoule { get { return RuleFor<string>(mobileExpression).Configure(x => { x.PropertyName = Lang.userShouJiHaoMa; }); } }
        private IRuleBuilderInitial<T, string> emailRoule { get { return RuleFor<string>(emailExpression).Configure(x => { x.PropertyName = Lang.userYouXiangDiZhi; }); } }
        private IRuleBuilderInitial<T, string> passwordRoule { get { return RuleFor<string>(passwordExpression).Configure(x => { x.PropertyName = Lang.userMiMa; }); } }

        public IRuleBuilderInitial<T, string> CheckAccount(bool checkLength = true)
        {
            accountExpression.CheckNull(nameof(accountExpression));
            accountRoule.CheckNull(nameof(accountRoule));

            if (checkLength)
            {
                accountRoule.Length(5, 36);
            }

            return accountRoule;
        }

        public IRuleBuilderInitial<T, string> CheckMobile(bool checkFormat = true)
        {
            mobileExpression.CheckNull(nameof(mobileExpression));
            mobileRoule.CheckNull(nameof(mobileRoule));

            if (checkFormat)
            {
                mobileRoule.Mobile();
            }
            return mobileRoule;
        }

        public IRuleBuilderInitial<T, string> CheckEmail(bool checkFormat = true)
        {
            emailExpression.CheckNull(nameof(emailExpression));
            emailRoule.CheckNull(nameof(emailRoule));

            if (checkFormat)
            {
                emailRoule.EmailAddress();
            }
            return emailRoule;
        }

        public IRuleBuilderInitial<T, string> CheckPassword(bool checkLength = false)
        {
            passwordExpression.CheckNull(nameof(passwordExpression));
            passwordRoule.CheckNull(nameof(passwordRoule));

            if (checkLength)
            {
                passwordRoule.Length(6, 36);
            }
            return passwordRoule;
        }
    }
}
