namespace UTH.Module.Core
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

    /// <summary>
    /// 验证码应用服务
    /// </summary>
    public class CaptchaApplication : ApplicationService, ICaptchaApplication
    {
        ISecurityCodeService securityCodeService;
        INotifyService notifyService;

        public CaptchaApplication(ISecurityCodeService securityCodeService, INotifyService notifyService, IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {
            this.securityCodeService = securityCodeService;
            this.notifyService = notifyService;
        }

        //默认验证码类型
        private const EnumSecurityCodeType _defaultSecurityCodeType = EnumSecurityCodeType.Random;

        /// <summary>
        /// 验证码发送
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CaptchaOutput Send(CaptchaInput input)
        {
            input.CheckNull();

            var key = GetCaptchaKey(input);

            var security = securityCodeService.Get(_defaultSecurityCodeType, input.Length, key);

            if (input.Mode == EnumCaptchaMode.Sms || input.Mode == EnumCaptchaMode.Email || input.Mode == EnumCaptchaMode.Voice)
            {
                SendNotify(input, security);
            }

            if (input.Mode == EnumCaptchaMode.Image)
            {

            }

            return EngineHelper.Map<CaptchaOutput>(security);
        }

        /// <summary>
        /// 验证码校验
        /// </summary>
        public bool Verify(CaptchaInput input)
        {
            input.CheckNull();

            var key = GetCaptchaKey(input);

            return securityCodeService.Verify(_defaultSecurityCodeType, key, input.Code);
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="input"></param>
        /// <param name="code"></param>
        private void SendNotify(CaptchaInput input, SecurityCodeModel security)
        {
            var notifyCategory = EnumNotifyCategory.Default;
            #region notifyCategory
            switch (input.Category)
            {
                case EnumCaptchaCategory.Regist:
                    notifyCategory = EnumNotifyCategory.注册验证;
                    break;
                case EnumCaptchaCategory.Login:
                    notifyCategory = EnumNotifyCategory.登录校验;
                    break;
                case EnumCaptchaCategory.FindPwd:
                    notifyCategory = EnumNotifyCategory.找回密码验证;
                    break;
            }
            #endregion

            var notifyMode = EnumNotifyMode.Default;
            #region notifyMode
            switch (input.Mode)
            {
                case EnumCaptchaMode.Sms:
                    notifyMode = EnumNotifyMode.短信;
                    break;
                case EnumCaptchaMode.Email:
                    notifyMode = EnumNotifyMode.邮件;
                    break;
                case EnumCaptchaMode.Voice:
                    notifyMode = EnumNotifyMode.语音;
                    break;
            }
            #endregion

            if (!notifyService.Send(notifyCategory, notifyMode, null, input.Receiver, security.Code))
            {
                securityCodeService.Remove(_defaultSecurityCodeType, GetCaptchaKey(input));
                throw new DbxException(EnumCode.提示消息, Lang.sysTongZhiFaSongCuoWu);
            }
        }

        /// <summary>
        /// 获取存储KEY
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetCaptchaKey(CaptchaInput input)
        {
            input.CheckNull();

            if (!input.Key.IsEmpty())
                return input.Key;

            return $"{input.Category}_{input.Mode}_{(!input.Receiver.IsEmpty() ? input.Receiver : Guid.NewGuid().ToString())}";
        }
    }
}
