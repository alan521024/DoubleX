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
    /// 账号注册
    /// </summary>
    public class RegistViewModel : UTHViewModel
    {
        public RegistViewModel() : base(culture.Lang.userZhuCe, "")
        {
            Initialize();
        }

        #region 私有变量

        #endregion

        #region 公共属性

        public string Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;
                Tag = "";
                Code = "";
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

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; RaisePropertyChanged(() => Tag); }
        }
        private string _tag;

        public string Code
        {
            get { return _code; }
            set { _code = value; RaisePropertyChanged(() => Code); }
        }
        private string _code;

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
        
        #endregion

        #region 辅助操作

        private void Initialize()
        {
            CanSend = false;
            IsRead = false;

            Mobile = string.Empty;
            Password = string.Empty;
            Tag = string.Empty;
            Code = string.Empty;
        }

        #endregion

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        public string SendCaptchaCode()
        {
            if (!Mobile.IsMobile())
            {
                return culture.Lang.userQingShuRuZhengQueDeShouJiHao;
            }

            Tag = ""; Code = "";

            var input = new CaptchaInput()
            {
                Category = EnumNotificationCategory.Regist,
                Type = EnumNotificationType.Sms,
                Receiver = Mobile,
            };

            var result = PlugCoreHelper.ApiUrl.Core.CaptchaSend.GetResult<CaptchaOutput, CaptchaInput>(input);
            if (result.Code == EnumCode.成功)
            {
                Tag = result.Obj.Tag;
                return null;
            }

            return result.Message;
        }

        /// <summary>
        /// 账号注册
        /// </summary>
        /// <returns></returns>
        public string Regist()
        {
            if (!Mobile.IsMobile())
            {
                return culture.Lang.userQingShuRuZhengQueDeShouJiHao;
            }

            if (Code.IsEmpty() || Tag.IsEmpty())
            {
                return culture.Lang.userQingShuRuYanZhengMa;
            }

            if (Password.IsEmpty())
            {
                return culture.Lang.userQingShuRuMiMa;
            }

            if (!IsRead)
            {
                return culture.Lang.userQingYueDuBingTongYiZhuCeXieYi;
            }

            var input = new RegistInput() { Mobile = Mobile, Password = Password, SmsCode = Code, Organize = Mobile };
            var result = PlugCoreHelper.ApiUrl.User.Regist.GetResult<RegistOutput, RegistInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return null;
            }

            return result.Message;
        }

    }
}