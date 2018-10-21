namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net;
    using System.Reflection;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Html;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Web 辅助类
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// 获取地址含返回路径
        /// </summary>
        /// <param name="context"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetUrl(HttpContext context, string path)
        {
            if (path.IsEmpty())
            {
                path = "";
            }
            return $"{path}{(path.IndexOf("?") > -1 ? "&" : "?")}ReturnUrl={GetQueryValue(context, "ReturnUrl")}";
        }

        /// <summary>
        /// 获取HttpContext
        /// </summary>
        /// <returns></returns>
        public static HttpContext GetContext()
        {
            return EngineHelper.Resolve<IHttpContextAccessor>().HttpContext;
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryValue(HttpContext context, string key)
        {
            if (context.IsNull() || key.IsEmpty())
                return "";
            var queryItem = context.Request.Query[key];
            if (queryItem.IsEmpty())
            {
                return "";
            }
            return StringHelper.Get(queryItem);
        }

        /// <summary>
        /// 根据名称获取请求方式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EnumHttpVerb GetHttpVerbByName(string name, EnumHttpVerb defaultValue = EnumHttpVerb.DEFAULT)
        {
            if (name.StartsWith("Find", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Find", StringComparison.InvariantCultureIgnoreCase))
            {
                return EnumHttpVerb.GET;
            }

            if (name.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Create", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Insert", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Update", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Modify", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("DeleteId", StringComparison.InvariantCultureIgnoreCase) ||
                name.StartsWith("Remove", StringComparison.InvariantCultureIgnoreCase))
            {
                return EnumHttpVerb.POST;
            }

            #region 设置仅支持get post 忽略 put delete patch ....

            //if (methodName.StartsWith("Put", StringComparison.InvariantCultureIgnoreCase) ||
            //    methodName.StartsWith("UpdateBatch", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return "PUT";
            //}

            //if (methodName.StartsWith("DeleteWhere", StringComparison.InvariantCultureIgnoreCase) ||
            //    methodName.StartsWith("Remove", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return "DELETE";
            //}

            //if (methodName.StartsWith("Patch", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return "PATCH";
            //}

            //if (methodName.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase) ||
            //    methodName.StartsWith("Create", StringComparison.InvariantCultureIgnoreCase) ||
            //    methodName.StartsWith("InsertBatch", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return "POST";
            //}

            #endregion

            return defaultValue;
        }


        /// <summary>
        /// 获取Web应用Id
        /// </summary>
        /// <returns></returns>
        public static string GetAppCode(HttpContext context = null)
        {
            var isSupper = IsWebSupperCookies();
            if (!isSupper)
                throw new DbxException(EnumCode.不支持Cookie);

            if (context.IsNull())
                context = EngineHelper.Resolve<IHttpContextAccessor>().HttpContext;

            if (context.IsNull())
                throw new DbxException(EnumCode.不支持Http);

            var request = context.Request;

            if (!request.Cookies["appcode"].IsEmpty())
                return request.Cookies["appcode"].ToString();

            if (!request.Query["appcode"].IsEmpty())
                return request.Query["appcode"].ToString();

            if (!request.Headers["appcode"].IsEmpty())
                return request.Headers["appcode"].ToString();

            return EngineHelper.Configuration.AppCode;
        }

        /// <summary>
        /// 获取ClientIp(参数,不是来源真实Ip)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(HttpContext context = null)
        {
            var isSupper = IsWebSupperCookies();
            if (!isSupper)
                throw new DbxException(EnumCode.不支持Cookie);

            if (context.IsNull())
                context = EngineHelper.Resolve<IHttpContextAccessor>().HttpContext;

            if (context.IsNull())
                throw new DbxException(EnumCode.不支持Http);

            var request = context.Request;

            if (!request.Cookies["ClientIp"].IsEmpty())
                return request.Cookies["ClientIp"].ToString();

            if (!request.Query["ClientIp"].IsEmpty())
                return request.Query["ClientIp"].ToString();

            if (!request.Headers["ClientIp"].IsEmpty())
                return request.Headers["ClientIp"].ToString();

            if (!request.Headers["X-Forwarded-For"].FirstOrDefault().IsEmpty())
                return request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!context.Connection.RemoteIpAddress.IsEmpty())
                return context.Connection.RemoteIpAddress.ToString();

            return "127.0.0.1";
        }

        /// <summary>
        /// 获取区域/环境多语言标识
        /// </summary>
        /// <returns></returns>
        public static string GetCulture(HttpContext context = null)
        {
            var isSupper = IsWebSupperCookies();
            if (!isSupper)
                throw new DbxException(EnumCode.不支持Cookie);

            if (context.IsNull())
                context = EngineHelper.Resolve<IHttpContextAccessor>().HttpContext;

            if (context.IsNull())
                throw new DbxException(EnumCode.不支持Http);

            var request = context.Request;

            if (!request.Cookies["culture"].IsEmpty())
                return request.Cookies["culture"].ToString();

            if (!request.Query["culture"].IsEmpty())
                return request.Query["culture"].ToString();

            if (!request.Headers["culture"].IsEmpty())
                return request.Headers["culture"].ToString();

            if (!EngineHelper.Configuration.Culture.IsEmpty())
                return EngineHelper.Configuration.Culture.Split(',').FirstOrDefault();

            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetToken(HttpContext context = null)
        {
            if (context.IsNull())
                context = EngineHelper.Resolve<IHttpContextAccessor>().HttpContext;

            if (context.IsNull())
                throw new DbxException(EnumCode.不支持Http);

            var request = context.Request;

            string token = string.Empty;

            if (!request.Headers["Authorization"].FirstOrDefault().IsEmpty())
            {
                token = StringHelper.FormatTrimStart(request.Headers["Authorization"].FirstOrDefault(), "Bearer ");
            }

            return token;
        }

        /// <summary>
        /// 设置区域/环境多语言
        /// </summary>
        /// <param name="context"></param>
        public static void SetCulture(HttpContext context)
        {
            //Accept-Language请求头允许客户端声明它可以理解的自然语言，以及优先选择的区域方言。借助内容协商机制，服务器可以从诸多备选项中选择一项进行应用 
            //并使用Content-Language 应答头通知客户端它的选择。浏览器会基于其用户界面语言来为这个请求头设置合适的值，即便是用户可以进行修改，
            //但是这种情况极少发生 (and is frown upon as it leads to fingerprinting)。

            var isSupper = IsWebSupperCookies();
            if (!isSupper)
                throw new DbxException(EnumCode.不支持Cookie);

            if (context.IsNull())
                throw new DbxException(EnumCode.不支持Http);

            var culture = GetCulture(context);

            if (!culture.IsEmpty())
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);    //设置当前语言区域
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);  //设置当前语言区域
            }
        }

        /// <summary>
        /// 判断是否ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers != null &&
                   request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// 是否支持Cookies
        /// </summary>
        /// <returns></returns>
        public static bool IsWebSupperCookies()
        {
            var cofnig = EngineHelper.Configuration;
            var isSupperCookies = false;

            switch (cofnig.AppType)
            {
                case EnumAppType.Web:
                case EnumAppType.Wap:
                case EnumAppType.Api:
                    isSupperCookies = true;
                    break;
                default:
                    isSupperCookies = false;
                    break;
            }
            return isSupperCookies;
        }

        /// <summary>
        /// 判断是否移动设备访问
        /// </summary>
        /// <returns></returns>                                                             
        public static bool IsMobileDevice()
        {
            String[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
            Boolean isMoblie = false;
            //if (HttpContext.Current.Request.UserAgent.ToString().ToLower() != null)
            //{
            //    for (int i = 0; i < mobileAgents.Length; i++)
            //    {
            //        if (HttpContext.Current.Request.UserAgent.ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
            //        {
            //            isMoblie = true;
            //            break;
            //        }
            //    }
            //}
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断返回结果是否为对象
        /// </summary>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public static bool IsWebObjectResult(Type returnType)
        {
            //Find the actual return type (unwrap Task)
            if (returnType == typeof(Task))
            {
                returnType = typeof(void);
            }
            else if (returnType.GetTypeInfo().IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GenericTypeArguments[0];
            }

            if (typeof(IActionResult).GetTypeInfo().IsAssignableFrom(returnType))
            {
                if (typeof(JsonResult).GetTypeInfo().IsAssignableFrom(returnType) || typeof(ObjectResult).GetTypeInfo().IsAssignableFrom(returnType))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置Response无缓存
        /// </summary>
        /// <param name="response"></param>
        public static void SetNoCache(HttpResponse response)
        {
            //Based on http://stackoverflow.com/questions/49547/making-sure-a-web-page-is-not-cached-across-all-browsers
            response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
            response.Headers["Pragma"] = "no-cache";
            response.Headers["Expires"] = "0";
        }

        /// <summary>
        /// 获取异常返回结果对象
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static ResultModel GetExceptionResult(Exception exception)
        {
            ResultModel model = new ResultModel();
            var dbxException = exception as DbxException;
            if (!dbxException.IsNull())
            {
                model.Code = dbxException.Code;
                model.Message = dbxException.Message;
            }
            else
            {
                model.Code = EnumCode.未知异常;
                model.Message = ExceptionHelper.GetMessage(exception);
            }
            return model;
        }

        /// <summary>
        /// 枚举Html代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static HtmlString GetEnumSelectHtml<T>(string id = null, string name = null, int? selected = null, KeyValueModel<string, int> first = null, bool isFirstNullHas = true)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                return null;
            }

            if (first.IsNull() && isFirstNullHas)
            {
                first = new KeyValueModel<string, int>("请选择", 0);
            }

            var build = new StringBuilder();

            build.AppendFormat("<select {0} {1}>", !id.IsNull() ? " id=\"" + id + "\" " : "", !name.IsNull() ? " name=\"" + name + "\" " : "");

            if (!first.IsNull())
            {
                build.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", first.Value, first.Key, !selected.IsNull() && selected.Value == first.Value ? "selected=\"selected\"" : "");
            }

            foreach (var item in Enum.GetValues(type))
            {
                if (StringHelper.Get(item).ToLower() == "default")
                    continue;

                int value = (int)item;
                build.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", value, item, !selected.IsNull() && selected.Value == value ? "selected=\"selected\"" : "");
            }

            build.AppendFormat("</select>");

            return new HtmlString(build.ToString());
        }

        /// <summary>
        /// 添加页面错误提示
        /// ModelState.AddModelError
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="key"></param>
        public static void AddPageError(this ControllerBase controller, string message, string key = null)
        {
            if (key.IsEmpty())
            {
                key = Guid.NewGuid().ToString();
            }
            controller.ModelState.AddModelError(key, message);
        }

    }
}
