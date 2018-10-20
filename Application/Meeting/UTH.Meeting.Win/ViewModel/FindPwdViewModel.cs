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
    /// 找回密码
    /// </summary>
    public class FindPwdViewModel : UTHViewModel
    {
        public FindPwdViewModel()
        {
            Mobile = string.Empty;
            Password = string.Empty;
            Password2 = string.Empty;
            CaptchaKey = string.Empty;
            CaptchaCode = string.Empty;

            CanSend = false;
            IsVerify = false;

            Setp0visible = true;
            Setp1visible = false;

            SendText = culture.Lang.sysHuoQuYanZhengMa;
        }

        public string Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;
                CaptchaKey = "";
                CaptchaCode = "";
                CanSend = Mobile.IsMobile();
                RaisePropertyChanged(() => Mobile);
            }
        }
        private string _mobile;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        private string _password;

        public string Password2
        {
            get { return _password2; }
            set
            {
                _password2 = value;
                RaisePropertyChanged(() => Password2);
            }
        }
        private string _password2;

        public bool CanSend
        {
            get { return _canSend; }
            set { _canSend = value; RaisePropertyChanged(() => CanSend); }
        }
        private bool _canSend;

        public bool IsVerify
        {
            get { return _isVerify; }
            set { _isVerify = value; RaisePropertyChanged(() => IsVerify); }
        }
        private bool _isVerify;

        public string CaptchaKey
        {
            get { return _captchaKey; }
            set { _captchaKey = value; RaisePropertyChanged(() => CaptchaKey); }
        }
        private string _captchaKey;

        public string CaptchaCode
        {
            get { return _captchaCode; }
            set { _captchaCode = value; RaisePropertyChanged(() => CaptchaCode); }
        }
        private string _captchaCode;

        public bool Setp0visible
        {
            get { return _setp0visible; }
            set { _setp0visible = value; RaisePropertyChanged(() => Setp0visible); }
        }
        private bool _setp0visible;

        public bool Setp1visible
        {
            get { return _setp1visible; }
            set { _setp1visible = value; RaisePropertyChanged(() => Setp1visible); }
        }
        private bool _setp1visible;

        public string SendText
        {
            get { return _sendText; }
            set { _sendText = value; RaisePropertyChanged(() => SendText); }
        }
        private string _sendText;

        /// <summary>
        /// 发送验证码事件
        /// </summary>
        public ICommand OnSendCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    SendCode();
                });
            }
        }

        /// <summary>
        /// 校验验证码事件
        /// </summary>
        public ICommand OnVerifyCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    VerifyCode();
                });
            }
        }

        public ICommand OnEditPwdCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    EditPwd();
                });
            }
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        public void SendCode()
        {
            if (!Mobile.IsMobile())
            {
                MessageAlert(culture.Lang.userQingShuRuYouXiaoDeShouJiHao);
            }

            var accountCheckName = PlugCoreHelper.ApiUrl.User.CheckName.GetResult<bool, AccountEditInput>(new AccountEditInput()
            {
                Mobile = Mobile
            });
            if (accountCheckName.Code != EnumCode.成功)
            {
                MessageAlert(accountCheckName.Message);
                return;
            }
            if (!accountCheckName.Obj)
            {
                MessageAlert(culture.Lang.userWeiZhaoDaoZhangHu);
                return;
            }

            var captchSend = PlugCoreHelper.ApiUrl.Core.CaptchaSend.GetResult<CaptchaOutput, CaptchaInput>(new CaptchaInput()
            {
                Category = EnumCaptchaCategory.FindPwd,
                Mode = EnumCaptchaMode.Sms,
                Receiver = Mobile
            });
            if (captchSend.Code != EnumCode.成功)
            {
                MessageAlert(captchSend.Message);
                return;
            }

            CaptchaKey = captchSend.Obj.Key;
            CaptchaCode = "";

            new Thread(() =>
            {
                ThreadHelper.Countdown((p) =>
                {
                    WpfHelper.ExcuteUI(() =>
                    {
                        var pro = Math.Abs((p / 1000) - AppHelper.CountdownSecond);
                        if (pro < 1)
                        {
                            CanSend = true;
                            SendText = culture.Lang.sysHuoQuYanZhengMa;
                        }
                        else
                        {
                            CanSend = false;
                            SendText = string.Format("{0}({1})", culture.Lang.sysDaoJiShi, pro);
                        }
                    });
                }, AppHelper.CountdownSecond * 1000);
            }).Start();
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <returns></returns>
        public void VerifyCode()
        {
            if (!Mobile.IsMobile() || CaptchaKey.IsEmpty() || CaptchaCode.IsEmpty())
            {
                MessageAlert(culture.Lang.sysYanZhengShiBai);
                return;
            }

            var captchVerify = PlugCoreHelper.ApiUrl.Core.CaptchaVerify.GetResult<bool, CaptchaInput>(new CaptchaInput()
            {
                Category = EnumCaptchaCategory.FindPwd,
                Mode = EnumCaptchaMode.Sms,
                Receiver = Mobile,
                Key = CaptchaKey,
                Code = CaptchaCode
            });

            if (captchVerify.Code != EnumCode.成功)
            {
                MessageAlert(captchVerify.Message ?? culture.Lang.sysYanZhengShiBai);
                return;
            }

            IsVerify = true;
            Setp0visible = false;
            Setp1visible = true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public void EditPwd()
        {
            if (!Mobile.IsMobile() || CaptchaKey.IsEmpty() || CaptchaCode.IsEmpty() || !IsVerify)
            {
                MessageAlert(culture.Lang.sysXiuGaiShiBai);
                return;
            }

            if (Password.IsEmpty())
            {
                MessageAlert(culture.Lang.userQingShuRuMiMa);
                return;
            }

            if (Password != Password2)
            {
                MessageAlert(culture.Lang.userXinMiMaYuQueRenMiMaBuTong);
                return;
            }

            var result = PlugCoreHelper.ApiUrl.User.FindPwd.GetResult<bool, FindPwdInput>(new FindPwdInput()
            {
                Mobile = Mobile,
                Password = Password,
                SmsCode = CaptchaCode
            });

            if (result.Code != EnumCode.成功)
            {
                MessageAlert(result.Message ?? culture.Lang.sysXiuGaiShiBai);
                return;
            }

            WpfHelper.Message(culture.Lang.sysXiuGaiChengGong, action: () =>
            {
                WpfHelper.GetParent<Window>(DependencyObj).FindChild<Frame>("mainFrame")
                    .Navigate(new UTH.Meeting.Win.View.Login());
            });
        }

    }
}