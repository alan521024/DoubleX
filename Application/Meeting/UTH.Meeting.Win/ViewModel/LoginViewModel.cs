namespace UTH.Meeting.Win.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    /// <summary>
    /// �˺ŵ�¼
    /// </summary>
    public class LoginViewModel : UTHViewModel
    {
        public LoginViewModel() : base(culture.Lang.userDengLu, "")
        {
            Initialize();
        }

        #region ˽�б���

        #endregion

        #region ��������

        public string Account
        {
            get { return _account; }
            set { _account = value; RaisePropertyChanged(() => Account); }
        }
        private string _account;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }
        private string _password;

        #endregion

        #region ��������

        private void Initialize()
        {
            Account = string.Empty;
            Password = string.Empty;
        }

        #endregion

        /// <summary>
        /// �˺ŵ�¼
        /// </summary>
        public string Signin()
        {
            if (Account.IsEmpty() || Password.IsEmpty())
            {
                return culture.Lang.userQingShuZhaoHaoMiMa;
            }

            var input = new SignInInput() { UserName = Account, Password = Password };
            var result = PlugCoreHelper.ApiUrl.User.SignIn.GetResult<SignInOutput, SignInInput>(input);
            if (result.Code == EnumCode.�ɹ�)
            {
                WpfHelper.SignIn(result.Obj.Token);
                return string.Empty;
            }

            return result.Message ?? culture.Lang.userDengLuShiBai;
        }
    }
}