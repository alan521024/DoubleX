namespace UTH.Meeting.Web.Areas.Wap.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.Helpers;
    using Senparc.Weixin.MP.Containers;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Module.Core;
    using Newtonsoft.Json.Linq;

    [Area("wap")]
    public class HomeController : WebViewBase
    {
        public IApplicationSession Current { get; set; }

        public IActionResult Index(string id = null)
        {
            if (Current.Accessor.Token.IsEmpty())
            {
                WebHelper.GetContext().SignOutAsync();
                return Redirect($"~/wap/home/sign?id={id}");
            }

            var model = new MeetingViewModel();


            model.ApiUrl = PlugCoreHelper.ApiUrl.Root;
            model.WebUrl = EngineHelper.Configuration.Settings.GetValue("WebUrl");
            model.RequestUrl = string.Format("{0}{1}{2}", model.WebUrl, HttpContext.Request.Path, HttpContext.Request.QueryString);


            string appId = EngineHelper.Configuration.Settings.GetValue("WeiXinAppId");
            string appSecret = EngineHelper.Configuration.Settings.GetValue("WeiXinSecret");
            model.WxJs = JSSDKHelper.GetJsSdkUiPackage(appId, appSecret, model.RequestUrl);

            var input = new MeetingEditInput()
            {
                Id = GuidHelper.Get(id)
            };

            ResultModel<MeetingDTO> result = null;

            try
            {
                result = PlugCoreHelper.ApiUrl.Meeting.MeetingGetId.GetResult<MeetingDTO, MeetingEditInput>(input);
            }
            catch
            {
                WebHelper.GetContext().SignOutAsync();
                return Redirect($"~/wap/home/sign?id={id}");
            }

            if (result.IsNull())
            {
                throw new DbxException(EnumCode.初始失败, "会议信息为空");
            }

            if (result.Code != EnumCode.成功)
            {
                throw new DbxException(EnumCode.初始失败);
            }

            if (result.Obj.IsNull())
            {
                throw new DbxException(EnumCode.初始失败, "未找到会议");
            }

            model.Meeting = result.Obj;

            if (!model.Meeting.IsNull() && !model.Meeting.Setting.IsEmpty())
            {
                model.Setting = JsonHelper.Deserialize<MeetingSettingModel>(model.Meeting.Setting);
            }

            return View(model);
        }

        public IActionResult Sign(string id = null)
        {
            var input = new SignInInput()
            {
                UserName = EngineHelper.Configuration.Settings.GetValue("DefaultUserName"),
                Password = EngineHelper.Configuration.Settings.GetValue("DefaultUserPwd")
            };

            var result = PlugCoreHelper.ApiUrl.User.SignIn.GetResult<SignInOutput, SignInInput>(input);
            if (result.Code == EnumCode.成功)
            {
                AppHelper.SignIn(result.Obj.Token);
            }
            else
            {
                throw new DbxException(EnumCode.认证失败, result.Message ?? UTH.Infrastructure.Resource.Culture.Lang.userDengLuShiBai);
            }

            return Redirect($"~/wap?id={id}");
        }

        public JsonResult Text(string source, string mediaId)
        {
            string speechText = "";
            string appId = EngineHelper.Configuration.Settings.GetValue("WeiXinAppId");
            string appSecret = EngineHelper.Configuration.Settings.GetValue("WeiXinSecret");

            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var downFileUrl = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken, mediaId);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(downFileUrl);
            httpWebRequest.Method = "GET";
            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    byte[] bytes = GetMemoryStream(responseStream).ToArray();
                    responseStream.Close();
                    var model = PlugCoreHelper.Speech(source, Convert.ToBase64String(bytes));
                    if (model != null)
                    {
                        if (model.Code == 0 && !string.IsNullOrEmpty(model.Obj))
                        {
                            speechText = model.Obj;
                        }
                        else
                        {

                            EngineHelper.LoggingError($"语音识别Error:OpenApi{model.Message ?? "未知错误"}");
                        }
                    }
                    else
                    {
                        EngineHelper.LoggingError($"语音识别Error:OpenApi返回对象Null");
                    }
                }
            }
            catch (Exception ex)
            {
                EngineHelper.LoggingError($"语音识别Error:{ExceptionHelper.GetMessage(ex)}");
            }

            return new JsonResult(speechText);
        }

        private MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }
    }
}