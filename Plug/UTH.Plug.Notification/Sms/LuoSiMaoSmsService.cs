namespace UTH.Plug.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 螺丝帽短信服务
    /// </summary>
    public class LuoSiMaoSmsService : ISmsService
    {
        public LuoSiMaoSmsService() { }

        public LuoSiMaoSmsService(IConfigObjService<SmsConfigModel> config)
        {
            var list = config.Load().Platforms;
            if (!list.IsEmpty())
            {
                platform = list.Find(x => x.Name == EnumSmsPlatform.LuoSiMao);
            }
        }

        private SmsPlatformModel platform { get; }

        public SmsSendOutput Send(string mobile, string content)
        {
            SmsSendOutput output = new SmsSendOutput();
            output.Success = false;

            if (!mobile.IsMobile() || content.IsEmpty())
            {
                output.Message = Lang.plugDuanXinHaoMaHuoNeiRongCuoWu;
                return output;
            }

            var formBody = string.Format("mobile={0}&message={1}", mobile, string.Format("{0} {1}", content, platform.Ident));
            string auth = string.Format("Basic {0}", Convert.ToBase64String(Encoding.Default.GetBytes(platform.Account + ":" + platform.Password)));

            var response = HttpHelper.Request(platform.Url, formBody, contentType: "application/x-www-form-urlencoded", action: (opt) =>
            {
                opt.Header = new WebHeaderCollection();
                opt.Header.Add("Authorization", auth);
            });

            EngineHelper.LoggingInfo(string.Format("sms result content : url:{0} data:{1} response:{2}", platform.Url, formBody,
                response.IsNull() ? "empty" : response.Content.IsEmpty() ? "empty" : response.Content));

            var model = JsonHelper.TryDeserialize<LuoSiMaoModel>(response?.Content);
            if (!model.IsNull())
            {
                output.Success = model.error == 0;
                output.Message = model.msg;
            }
            return output;
        }

        public Task<SmsSendOutput> SendAsync(string mobile, string content)
        {
            //TODO: LUOSIMAO SMS SendAsync
            return Task.FromResult<SmsSendOutput>(new SmsSendOutput() { });
        }
    }
}
