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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using MahApps.Metro.Controls;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    /// <summary>
    /// 账号登录
    /// </summary>
    public class LoginViewModel : UTHViewModel
    {
        public LoginViewModel() : base(culture.Lang.userDengLu, "")
        {
            Account = string.Empty;
            Password = string.Empty;
        }

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

        /// <summary>
        /// 登录事件
        /// </summary>
        public ICommand OnLoginCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Signin();
                });
            }
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        public void Signin()
        {
            if (Account.IsEmpty() || Password.IsEmpty())
            {
                MessageAlert(culture.Lang.userQingShuZhaoHaoMiMa);
                return;
            }

            var input = new SignInInput() { UserName = Account, Password = Password };
            var result = PlugCoreHelper.ApiUrl.User.SignIn.GetResult<SignInOutput, SignInInput>(input);
            if (result.Code != EnumCode.成功)
            {
                MessageAlert(result.Message ?? culture.Lang.userDengLuShiBai);
                return;
            }

            if (!(result.Obj.Type == EnumAccountType.组织 || result.Obj.Type == EnumAccountType.人员))
            {
                MessageAlert(culture.Lang.userZhangHuLeiXingCuoWu);
                return;
            }

            WpfHelper.SignIn(result.Obj.Token);
            ToMain();
        }

        private void ToMain()
        {
            View.Main mainForm = new View.Main();
            mainForm.Show();
            DependencyObj.GetParent().Close();
        }
    }
}