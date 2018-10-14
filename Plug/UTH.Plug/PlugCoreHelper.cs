namespace UTH.Plug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.IO;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 插件核心辅助操作
    /// </summary>
    public static class PlugCoreHelper
    {
        #region Lang

        /// <summary>
        /// 转换语言标识(UTH标识)
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string ConvertLang(this string lang)
        {
            if (lang.IsEmpty())
                return lang;

            lang = lang.ToLower();

            var langFormat = string.Format("|{0}|", lang);

            if ("|zs|zh-cn|zh-sg|zh-chs|zh".Contains(langFormat))
                return "zs";

            if ("|en|en-us|en-gb|en-au|en-bz|en-ca|en-cb|en-ie|en-jm|en-nz|en-ph|en-za|en-tt|en-zw|".Contains(langFormat))
                return "en";

            if ("|zh-tw|zh-hk|zh-mo|zh-cht|".Contains(langFormat))
                return "zt";

            if ("|pt|pt-br|pt-pt|".Contains(langFormat))
                return "pt";

            if ("|es|es-es|es|es-ar|es-bo|es-cl|es-co|es-cr|es-do|es-ec|es-sv|es-gt|es-hn|es-mx|es-ni|es-pa|es-py|es-pe|es-pr|es-es|es-uy|es-ve|".Contains(langFormat))
                return "es";

            if ("|fr|fr-fr|fr-be|fr-ca|fr-lu|fr-mc|fr-ch|".Contains(langFormat))
                return "fr";

            if ("|it|it-it|it-ch|".Contains(langFormat))
                return "it";

            if ("|ko|ko-kr|".Contains(langFormat))
                return "ko";

            if ("|ja|ja-jp|".Contains(langFormat))
                return "ja";

            if ("|de|de-de|de-at|de-li|de-lu|de-ch|".Contains(langFormat))
                return "de";

            if ("|ru|ru-ru|".Contains(langFormat))
                return "ru";

            if ("|ar|ar-ae|ar-dz|ar-bh|ar-eg|ar-iq|ar-jo|ar-kw|ar-lb|ar-ly|ar-ma|ar-qa|ar-sa|ar-ye|".Contains(langFormat))
                return "ar";

            if ("|bg|bg-bg|".Contains(langFormat))
                return "bg";

            if ("|ca|".Contains(langFormat))
                return "bg";

            if ("|cs|cs-cz|".Contains(langFormat))
                return "cs";

            if ("|DA|DA-DK|".Contains(langFormat))
                return "da";

            if ("|nl|nl-nl|nl-be|".Contains(langFormat))
                return "nl";

            if ("|et|et-ee|".Contains(langFormat))
                return "nl";

            if ("|fi|fi-fi|".Contains(langFormat))
                return "fi";

            if ("|el|el-gr|".Contains(langFormat))
                return "el";

            if ("|ht|".Contains(langFormat))
                return "ht";

            if ("|he|he-il|".Contains(langFormat))
                return "he";

            if ("|hi|hi-in|".Contains(langFormat))
                return "hi";

            if ("|mww|".Contains(langFormat))
                return "mww";

            if ("|hu|hu-hu|".Contains(langFormat))
                return "hu";

            if ("|id|id-id|".Contains(langFormat))
                return "hu";

            if ("|lv|lv-lv|".Contains(langFormat))
                return "lv";

            if ("|lt|lt-lt|".Contains(langFormat))
                return "lt";

            if ("|no|nb-no|nn-no|".Contains(langFormat))
                return "no";

            if ("|fa|fa-ir|".Contains(langFormat))
                return "fa";

            if ("|pl|pl-pl|".Contains(langFormat))
                return "pl";

            if ("|ro|ro-ro|".Contains(langFormat))
                return "ro";

            if ("|th|th-th|".Contains(langFormat))
                return "tr";

            if ("|tr|tr-tr|".Contains(langFormat))
                return "tr";

            if ("|uk|uk-ua|".Contains(langFormat))
                return "uk";

            if ("|vi|vi-vn|".Contains(langFormat))
                return "vi";

            if ("|sv|sv-se|sv-fi|".Contains(langFormat))
                return "sv";

            if ("|sl|sl-si|".Contains(langFormat))
                return "sl";

            if ("|sk-sk|sk|".Contains(langFormat))
                return "sk";

            if ("|mt|mt-mt|".Contains(langFormat))
                return "mt";

            if ("|lo|lo-la|".Contains(langFormat))
                return "lo";

            if ("|km|km-kh|".Contains(langFormat))
                return "km";

            return lang;
        }

        #endregion

        #region Api Server 

        #region 静态全局属性

        /// <summary>
        /// Api Server 认证失败处理
        /// </summary>
        public static Action<string> ApiServerAuthError { get; set; }

        /// <summary>
        /// Api Server 认证过期处理
        /// </summary>
        public static Action<string, string> ApiServerAuthExpire { get; set; }

        /// <summary>
        /// Api 结果 != EnumCode.成功 处理
        /// </summary>
        public static Action<EnumCode, string, dynamic> ApiServerResultError { get; set; }

        #endregion

        #region ApiUrl地址

        public static ApiUrlConfigModel ApiUrl { get { return ApiUrlConfigObjService.Instance; } }

        /// <summary>
        /// 获取Api Server 接口地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetApiUrl(this string url)
        {
            if (!url.IsEmpty() && url.ToLower().IndexOf("://") == -1)
            {
                url = string.Format("{0}{1}", ApiUrl.Root, url);
            }
            return url;
        }

        #endregion

        #region Api Result

        /// <summary>
        /// ApiServer 请求
        /// </summary>
        public static ResultModel<TModel> GetResult<TModel, TParam>(this string url, TParam post = default(TParam), string contentType = "application/json",
            string culture = null, string appCode = null, string clientIp = null, string token = null)
        {
            return GetResult<TModel>(url, post: (post.IsNull() ? "" : JsonHelper.Serialize(post)), contentType: contentType,
                culture: culture, appCode: appCode, clientIp: clientIp, token: token);
        }

        /// <summary>
        /// ApiServer 请求
        /// </summary>
        public static ResultModel<TModel> GetResult<TModel, TParam>(this string url, IApplicationSession session, TParam post = default(TParam), string contentType = "application/json")
        {
            return GetResult<TModel>(url, post: (post.IsNull() ? "" : JsonHelper.Serialize(post)), contentType: contentType,
                culture: session?.Accessor.Culture.Name, appCode: session?.Accessor.AppCode, clientIp: session?.Accessor.ClientIp, token: session?.Accessor.Token);

        }

        /// <summary>
        /// ApiServer 请求
        /// </summary>
        public static ResultModel<TModel> GetResult<TModel>(this string url, IApplicationSession session, string post = "", string contentType = "application/json")
        {
            return GetResult<TModel>(url, post: post, contentType: contentType,
                culture: session?.Accessor.Culture.Name, appCode: session?.Accessor.AppCode, clientIp: session?.Accessor.ClientIp, token: session?.Accessor.Token);

        }

        /// <summary>
        /// ApiServer 请求
        /// </summary>
        public static ResultModel<TModel> GetResult<TModel>(this string url, string post = "", string contentType = "application/json",
            string culture = null, string appCode = null, string clientIp = null, string token = null)
        {
            var session = EngineHelper.Resolve<IApplicationSession>();
            if (!session.IsNull())
            {
                culture = culture.IsEmpty() && !session.Accessor.Culture.IsNull() ? session.Accessor.Culture.Name : culture;
                appCode = appCode.IsEmpty() && !session.Accessor.AppCode.IsEmpty() ? session.Accessor.AppCode : appCode;
                clientIp = clientIp.IsEmpty() && !session.Accessor.ClientIp.IsEmpty() ? session.Accessor.ClientIp : clientIp;
                token = token.IsEmpty() && !session.Accessor.Token.IsEmpty() ? session.Accessor.Token : token;
            }

            if (!url.IsEmpty() && url.ToLower().IndexOf("://") == -1)
            {
                url = string.Format("{0}{1}", ApiUrl.Root, url);
            }

            var result = HttpHelper.Request<ResultModel<TModel>>(url, post: post, contentType: contentType, action: (option) =>
            {
                option.Header = new WebHeaderCollection();
                option.Header.Add("Culture", culture);
                option.Header.Add("AppCode", appCode);
                option.Header.Add("ClientIp", clientIp);
                if (!token.IsEmpty())
                {
                    option.Header.Add("Authorization", string.Format("Bearer {0}", token));
                }
            });

            //TODO: 接口结果 认证出错处理  权限/授权出错处理 其它错误处理
            switch (result.Code)
            {
                case EnumCode.认证失败:
                    ApiServerAuthError?.Invoke(token);
                    break;
                case EnumCode.认证过期:
                    if (!ApiServerAuthExpire.IsNull() && url.ToLower().IndexOf(ApiUrl.User.Refresh.ToLower()) == -1)
                    {
                        var newToken = string.Empty;
                        var tokenResult = GetResult<string>(ApiUrl.User.Refresh, post: JsonHelper.Serialize(new { Token = token }), contentType: contentType);
                        if (tokenResult.Code == EnumCode.成功)
                        {
                            newToken = tokenResult.Obj;
                        }
                        ApiServerAuthExpire(token, newToken);
                    }
                    break;
            }

            //接口Result Code 错误
            if (result.Code != EnumCode.成功 && !ApiServerResultError.IsNull())
            {
                ApiServerResultError?.Invoke(result.Code, result.Message, result.Obj);
            }

            return result;
        }

        #endregion

        #endregion

        #region Open Api 

        #region Translation

        /// <summary>
        /// 文本翻译(Get)
        /// </summary>
        public static List<OpenApiTranslationModel> TranslationGet(string content, string sourceLangue, string targetLangues)
        {
            OpenApiConfigObjService.Instance.CheckNull();

            List<OpenApiTranslationModel> list = new List<OpenApiTranslationModel>();

            var post = new JObject();
            post["key"] = OpenApiConfigObjService.Instance.ApiKey;
            post["source"] = GetTranslationLang(sourceLangue);
            post["target"] = GetTranslationLang(targetLangues);
            post["q"] = content;
            //post["quality"] = "80";
            //post["domain"] = "";

            var trsUrl = $"{OpenApiConfigObjService.Instance.ApiUrl}{OpenApiConfigObjService.Instance.TranslationGet}";
            var trsPost = JsonHelper.Serialize(post);
            var resultContent = HttpHelper.Request(string.Format("{0}{1}", OpenApiConfigObjService.Instance.ApiUrl, OpenApiConfigObjService.Instance.TranslationGet), JsonHelper.Serialize(post));

            EngineHelper.LoggingInfo($"OpenApiTranslation  url:{trsUrl} post:{trsPost} result:{resultContent?.Content}");

            var resultModel = JsonHelper.TryDeserialize<OpenApiTranslationOutput>(resultContent?.Content);
            if (!resultModel.IsNull() && resultModel.Code == 0)
            {
                list = resultModel.Translations;
            }
            return list;
        }

        /// <summary>
        /// 获取语音(翻译接口使用)
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetTranslationLang(string lang, string split = "|")
        {
            if (!lang.IsEmpty())
            {
                var arr = StringHelper.GetToArray(lang, new string[] { split });
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] = ConvertLang(arr[i]);
                }
                return string.Join("|", arr);
            }
            return lang;
        }

        #endregion

        #region voiceTotext

        /// <summary>
        /// Base64语音内容识别
        /// </summary>
        /// <param name="source"></param>
        /// <param name="base64"></param>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static OpenApiSpeechModel Speech(string source, string base64, string format = "amr", int rate = 8000, string platform = "google")
        {
            OpenApiConfigObjService.Instance.CheckNull();

            List<OpenApiTranslationModel> list = new List<OpenApiTranslationModel>();

            var post = new JObject();
            post["key"] = OpenApiConfigObjService.Instance.ApiKey;
            post["format"] = format;
            post["source"] = source;
            post["Rate"] = 8000;
            post["content"] = base64;
            post["Platform"] = platform;

            var resultContent = HttpHelper.Request(string.Format("{0}{1}", OpenApiConfigObjService.Instance.ApiUrl, OpenApiConfigObjService.Instance.Speech), JsonHelper.Serialize(post));
            return JsonHelper.TryDeserialize<OpenApiSpeechModel>(resultContent?.Content);
        }

        #endregion

        #endregion

        #region WeiXin

        //public static void test()
        //{

        //    string appID = "wx8eff62aca23ebf87";
        //    string appSecret = "b224033b6b4bf36970deaf5aa846ab4d";

        //    string serverId = "z2hpK9A2SvYf0Kb501HGFQ59spXSJVczjP7eE0JRGNcgUUDEsNpKArUWI6OQsgS8";

        //    var accessToken = AccessTokenContainer.TryGetAccessToken(appID, appSecret);
        //    var fileUrl = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken, serverId);

        //    var fileClient = new HttpWebClientUtility();
        //    var fileResult = fileClient.Request(new HttpClientOptions() { URL = fileUrl, Method = "GET" });
        //    var fileStr = System.Text.Encoding.UTF8.GetString(fileResult.Bytes);


        //    List<KeyValueModel> config = new List<KeyValueModel>();
        //    config.Add(new KeyValueModel("encoding", ""));
        //    config.Add(new KeyValueModel("sampleRateHertz", ""));
        //    config.Add(new KeyValueModel("languageCode", ""));

        //    List<KeyValueModel> audio = new List<KeyValueModel>();
        //    //audio.Add(new KeyValueModel("uri", ""));
        //    audio.Add(new KeyValueModel("content", ""));

        //    var post = JsonHelper.Serialize(new { config = config, audio = audio });

        //    string trsUrl = "http://47.90.5.180:57777/v1/speech:recognize?key=AIzaSyAp9zTdK70Er0RcZ-Jser6rll7S7AyKw7k";
        //    var trsClient = new HttpWebClientUtility();
        //    var trsResult = fileClient.Request(new HttpClientOptions() { URL = fileUrl, Method = "GET" });
        //    var trsStr = System.Text.Encoding.UTF8.GetString(fileResult.Bytes);


        //    //string url = System.Configuration.ConfigurationManager.AppSettings["GoogleSpeechApiUrl"] + "?key=" +
        //    //    System.Configuration.ConfigurationManager.AppSettings["GoogleApiVinceKey"];

        //    //var tokenResult = HttpAPI.PostWebRequest(url, src, "application/json");

        //    //isSeccess = false;
        //    //var baiduToken = new ApiToken();
        //    //var configObj = new JObject(){
        //    //                     {"encoding", input.Format.ToUpper()},
        //    //                     {"sampleRateHertz", input.Rate},
        //    //                     {"languageCode", LanguageComm.GetGoogleLangCode(input.Lan)},
        //    //                };
        //    //var audioObj = new JObject();
        //    //if (!string.IsNullOrEmpty(input.Url))
        //    //    audioObj.Add("uri", input.Url);
        //    //if (!string.IsNullOrEmpty(input.Speech))
        //    //    audioObj.Add("content", input.Speech);

        //    //var targetData = new JObject()
        //    //{
        //    //     {"config", configObj},
        //    //     {"audio",  audioObj}
        //    //};
        //    //string src = targetData.ToString();
        //    ////string url = "http://47.90.5.180:57777/v1/speech:recognize?key=AIzaSyAp9zTdK70Er0RcZ-Jser6rll7S7AyKw7k";
        //    //string url = System.Configuration.ConfigurationManager.AppSettings["GoogleSpeechApiUrl"] + "?key=" +
        //    //    System.Configuration.ConfigurationManager.AppSettings["GoogleApiVinceKey"];

        //    //var tokenResult = HttpAPI.PostWebRequest(url, src, "application/json");
        //    //try
        //    //{
        //    //    var obj = (JObject)JsonConvert.DeserializeObject(tokenResult);
        //    //    if (obj.GetValue("results") != null && obj["results"][0] != null && obj["results"].FirstOrDefault()["alternatives"] != null && obj["results"].FirstOrDefault()["alternatives"].FirstOrDefault() != null)
        //    //    {
        //    //        string transcript = obj["results"].FirstOrDefault()["alternatives"].FirstOrDefault()["transcript"].ToString();
        //    //        string confidence = obj["results"].FirstOrDefault()["alternatives"].FirstOrDefault()["confidence"].ToString();
        //    //        isSeccess = true;
        //    //        return transcript;

        //    //    }
        //    //    return "";
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    return e.Message;
        //    //}




        //    //string result = "";
        //    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
        //    //httpWebRequest.Method = "GET";

        //    //HttpWebResponse httpWebResponse;
        //    //try
        //    //{
        //    //    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //    //    using (Stream responseStream = httpWebResponse.GetResponseStream())
        //    //    {
        //    //        byte[] bytes = GetMemoryStream(responseStream).ToArray();
        //    //        responseStream.Close();

        //    //        string base64String = Convert.ToBase64String(bytes);

        //    //        var obj = new JObject
        //    //        {
        //    //            {"Key" , "UTHInnerKey_20170209" },
        //    //            {"format" , "amr" },
        //    //            {"source", "zh" },
        //    //            { "content", base64String}
        //    //        };
        //    //        var res = APIBase.PostHttp("http://192.168.50.251:6640/api/voice/text", obj.ToString(), "application/json");
        //    //    }
        //    //}
        //    //catch (WebException ex)
        //    //{
        //    //}
        //    //return new JsonResult(result);

        //}

        #endregion
    }
}