namespace UTH.Meeting.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.Helpers;
    using Senparc.Weixin.MP.Containers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Module.Core;

    public class CommonController : Controller
    {
        /// <summary>
        /// 执行Api
        /// </summary>
        public ResultModel<object> Do()
        {
            string apiUrl = null, apiBody = null;
            using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
            {
                var json = JsonHelper.Deserialize<JObject>(reader.ReadToEnd());
                apiUrl = CodingHelper.UrlDecode(StringHelper.Get(json["action"]));
                apiBody = StringHelper.Get(json["content"]);
            }
            apiUrl.CheckEmpty();

            return apiUrl.GetResult<object>(apiBody);
        }
    }
}
