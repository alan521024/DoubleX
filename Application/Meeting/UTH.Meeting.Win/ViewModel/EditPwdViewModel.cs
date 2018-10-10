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

    public class EditPwdViewModel : UTHViewModel
    {
        public EditPwdViewModel() : base(culture.Lang.userXiuGaiMiMa, "")
        {
            Initialize();
        }

        #region 私有变量

        #endregion

        #region 公共属性

        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; RaisePropertyChanged(() => OldPassword); }
        }
        private string _oldPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; RaisePropertyChanged(() => NewPassword); }
        }
        private string _newPassword;

        public string AffirmPassword
        {
            get { return _affirmPassword; }
            set { _affirmPassword = value; RaisePropertyChanged(() => AffirmPassword); }
        }
        private string _affirmPassword;

        #endregion

        #region 辅助操作

        private void Initialize()
        {
        }

        #endregion

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string EditPwd()
        {
            var input = new EditPwdInput()
            {
                AccountId = CurrentUser.User.Id,
                OldPassword = OldPassword,
                NewPassword = NewPassword,
                AffirmPassword = AffirmPassword
            };

            var verify = new EditPwdInputValidator().Validate(input);
            if (!verify.IsValid)
            {
                return verify.VerifyFirstMessage() ?? culture.Lang.sysYanZhengShiBai;
            }

            var result = PlugCoreHelper.ApiUrl.User.EditPwd.GetResult<object, EditPwdInput>(input);
            if (result.Code != EnumCode.成功)
            {
                return result.Message;
            }

            OldPassword = string.Empty;
            NewPassword = string.Empty;
            AffirmPassword = string.Empty;

            return string.Empty;
        }
    }
}