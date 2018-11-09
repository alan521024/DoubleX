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
    /// �˺�ע��
    /// </summary>
    public class RegistViewModel : UTHViewModel
    {
        public RegistViewModel() : base(culture.Lang.userZhuCe, "")
        {
            Mobile = string.Empty;
            Password = string.Empty;

            CaptchaKey = "";
            CaptchaCode = "";

            CanSend = false;
            IsRead = false;
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

        public string SendText
        {
            get { return _sendText; }
            set { _sendText = value; RaisePropertyChanged(() => SendText); }
        }
        private string _sendText = culture.Lang.sysHuoQuYanZhengMa;

        public bool CanSend
        {
            get { return _canSend; }
            set { _canSend = value; RaisePropertyChanged(() => CanSend); }
        }
        private bool _canSend = false;

        public bool IsRead
        {
            get { return _isRead; }
            set { _isRead = value; RaisePropertyChanged(() => IsRead); }
        }
        private bool _isRead = false;

        /// <summary>
        /// ������֤���¼�
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
        /// ע���¼�
        /// </summary>
        public ICommand OnRegistCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Regist(obj);
                });
            }
        }


        /// <summary>
        /// ������֤��
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
            if (accountCheckName.Code != EnumCode.�ɹ�)
            {
                MessageAlert(accountCheckName.Message);
                return;
            }
            if (accountCheckName.Obj)
            {
                MessageAlert(culture.Lang.userZhangHuYiCunZai);
                return;
            }
            var captchSend = PlugCoreHelper.ApiUrl.Core.CaptchaSend.GetResult<CaptchaOutput, CaptchaInput>(new CaptchaInput()
            {
                Category = EnumCaptchaCategory.Regist,
                Mode = EnumCaptchaMode.Sms,
                Receiver = Mobile
            });

            if (captchSend.Code != EnumCode.�ɹ�)
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
        /// �˺�ע��
        /// </summary>
        /// <returns></returns>
        public void Regist(object obj)
        {
            if (!Mobile.IsMobile())
            {
                MessageAlert(culture.Lang.userQingShuRuYouXiaoDeShouJiHao);
                return;
            }

            if (CaptchaKey.IsEmpty() || CaptchaCode.IsEmpty())
            {
                MessageAlert(culture.Lang.userQingShuRuYanZhengMa);
                return;
            }

            if (Password.IsEmpty())
            {
                MessageAlert(culture.Lang.userQingShuRuMiMa);
                return;
            }

            if (!IsRead)
            {
                MessageAlert(culture.Lang.userQingYueDuBingTongYiZhuCeXieYi);
                return;
            }

            var input = new RegistInput()
            {
                Mobile = Mobile,
                Password = Password,
                SmsCode = CaptchaCode,
                SmsCodeKey = CaptchaKey,
                Organize = Mobile
            };
            var result = PlugCoreHelper.ApiUrl.User.Regist.GetResult<RegistOutput, RegistInput>(input);
            if (result.Code != EnumCode.�ɹ�)
            {
                MessageAlert(!result.Message.IsEmpty() ? result.Message : culture.Lang.userZhuCeShiBai);
                return;
            }

            WpfHelper.Message(culture.Lang.userZhuCeChengGongXiaoXi, action: () =>
            {
                WpfHelper.GetParent<Window>(DependencyObj).FindChild<Frame>("mainFrame")
                    .Navigate(new UTH.Meeting.Win.View.Login());
            });
        }

    }
}