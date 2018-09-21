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
    using UTH.Plug;

    /// <summary>
    /// 验证码业务
    /// </summary>
    public class CaptchaService : ApplicationService, ICaptchaService
    {
        private char[] numberCodeContent = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public CaptchaService() { }

        /// <summary>
        /// 验证码发送/获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CaptchaOutput Send(CaptchaSendInput input)
        {
            input.Tag = GuidHelper.GetToString(Guid.NewGuid());

            string captchaKey = GetCaptchaKey(input);

            captchaKey.CheckEmpty();

            CaptchaOutput result = SessionCaching.Get<CaptchaOutput>(captchaKey);
            if (!result.IsNull())
            {
                return result;
            }

            result = new CaptchaOutput();
            result.Tag = input.Tag;
            result.Code = GetCaptchaCode(input);
            if (EngineHelper.Configuration.CaptchaExpire > 0)
            {
                SessionCaching.Set(captchaKey, result, TimeSpan.FromSeconds(EngineHelper.Configuration.CaptchaExpire));
            }
            else
            {
                SessionCaching.Set(captchaKey, result);
            }

            if (input.Type == EnumNotificationType.Sms || input.Type == EnumNotificationType.Email || input.Type == EnumNotificationType.Voice)
            {
                var status = CaptchaNotification(input.Category, input.Type, input.Receiver, result.Code);
                if (status.IsNull() || (!status.IsNull() && !status.Success))
                {
                    SessionCaching.Remove(captchaKey);
                    throw new DbxException(EnumCode.提示消息, Lang.sysTongZhiFaSongCuoWu);
                }
            }

            return result;
        }

        /// <summary>
        /// 验证码校验
        /// </summary>
        public bool Verify(CaptchaVerifyInput input)
        {
            string captchaKey = GetCaptchaKey(input);
            captchaKey.CheckEmpty();

            var captchaModel = SessionCaching.Get<CaptchaOutput>(captchaKey);
            if (captchaModel.IsNull())
            {
                return false;
            }

            return StringHelper.IsEqual(input.Code, captchaModel.Code, ignoreCase: true);
        }

        /// <summary>
        /// 验证码移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(CaptchaVerifyInput input)
        {
            string captchaKey = GetCaptchaKey(input);
            if (!captchaKey.IsEmpty())
            {
                SessionCaching.Remove(captchaKey);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证码内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetCaptchaCode(CaptchaSendInput input)
        {
            input.CheckNull();

            //image
            if (input.Type == EnumNotificationType.Image)
            {
                return RandomHelper.GetToRandomString(input.Length);
            }

            //sms
            if (input.Type == EnumNotificationType.Sms)
            {
                return RandomHelper.GetToRandomString(input.Length, contentChars: numberCodeContent);
            }

            throw new ArgumentNullException(string.Format("{0} {1}", nameof(input.Category), nameof(input.Type)));
        }

        /// <summary>
        /// 获取缓存Key
        /// </summary>
        private string GetCaptchaKey(CaptchaInput input)
        {
            input.CheckNull();
            return string.Format("{0}_{1}_{2}", input.Category, input.Type, input.Type == EnumNotificationType.Sms ? input.Receiver : input.Tag);
        }

        /// <summary>
        /// 验证码通知
        /// </summary>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <param name="receiver"></param>
        private NotificationOutput CaptchaNotification(EnumNotificationCategory category, EnumNotificationType type, string receiver, string code)
        {
            var output = new NotificationOutput() { Success = false };

            if (!(type == EnumNotificationType.Sms || type == EnumNotificationType.Image))
            {
                output.Message = Lang.sysTongZhiLeiXinCuoWu;
                return output;
            }

            NotificationInput input = new NotificationInput()
            {
                Category = category,
                Type = type,
                Receiver = receiver,
                Content = ""
            };

            if (category == EnumNotificationCategory.Regist)
            {
                input.Content = "注册 验证码为 " + code + " ";
            }

            if (category == EnumNotificationCategory.FindPwd)
            {
                input.Content = "找回密码 验证码为 " + code + " ";
            }

            if (!input.IsNull() && !input.Content.IsEmpty())
            {
                output = EngineHelper.Resolve<INotificationService>().Send(input);
            }

            return output;
        }
    }
}
