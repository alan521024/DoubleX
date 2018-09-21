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
    /// 找回密码
    /// </summary>
    public class FindPwdViewModel : UTHViewModel
    {
        public FindPwdViewModel()
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

        #endregion

        #region 辅助操作

        private void Initialize()
        {

            Mobile = string.Empty;
            Password = string.Empty;
            Password2 = string.Empty;
            Tag = string.Empty;
            Code = string.Empty;

            CanSend = false;
            IsVerify = false;

            Setp0visible = true;
            Setp1visible = false;

            SendText = culture.Lang.sysHuoQuYanZhengMa;
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

            var input = new CaptchaSendInput(EnumNotificationCategory.FindPwd, EnumNotificationType.Sms, Mobile);
            var result = PlugCoreHelper.ApiUrl.Core.CaptchaSend.GetResult<CaptchaOutput, CaptchaSendInput>(input);
            if (result.Code == EnumCode.成功)
            {
                Tag = result.Obj.Tag;
                return null;
            }
            return result.Message;
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <returns></returns>
        public string VerifyCaptchaCode()
        {
            if (!Mobile.IsMobile())
            {
                return culture.Lang.userQingShuRuZhengQueDeShouJiHao;
            }

            if (Code.IsEmpty())
            {
                return culture.Lang.userQingShuRuYanZhengMa;
            }

            var input = new CaptchaVerifyInput(EnumNotificationCategory.FindPwd, EnumNotificationType.Sms);
            input.Receiver = Mobile;
            input.Code = Code;
            input.Tag = Tag;

            var result = PlugCoreHelper.ApiUrl.Core.CaptchaVerify.GetResult<bool, CaptchaVerifyInput>(input);

            if (result.Code != EnumCode.成功)
            {
                return result.Message;
            }

            if (!result.Obj)
            {
                return result.Message.IsEmpty() ? "验证失败" : result.Message;
            }

            IsVerify = true;
            Setp0visible = false;
            Setp1visible = true;

            return null;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string EditPwd()
        {
            if (!Mobile.IsMobile())
            {
                return culture.Lang.userQingShuRuZhengQueDeShouJiHao;
            }

            if (Code.IsEmpty() || Tag.IsEmpty())
            {
                return culture.Lang.userQingShuRuYanZhengMa;
            }

            if (!IsVerify)
            {
                return culture.Lang.userYanZhengMaCuoWu;
            }

            if (Password.IsEmpty())
            {
                return culture.Lang.userQingShuRuMiMa;
            }

            var input = new FindPwdInput() { Mobile = Mobile, Password = Password, SmsCode = Code };
            var result = PlugCoreHelper.ApiUrl.User.FindPwd.GetResult<bool, FindPwdInput>(input);
            if (result.Code != EnumCode.成功)
            {
                return result.Message;
            }

            return null;
        }

    }
}