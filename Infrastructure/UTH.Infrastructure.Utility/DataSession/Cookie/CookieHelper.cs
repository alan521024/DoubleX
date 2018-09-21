using System;
using System.Net;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Cookie工具类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 获取Cookie(根据CookieKey)
        /// </summary>
        /// <param name="key">Cookie Key</param>
        /// <returns>返回string</returns>
        public static string Get(HttpWebRequest request, string key, string path = "/")
        {
            var cookies = request.CookieContainer.GetCookies(new Uri("/"));
            if (cookies != null && cookies[key] != null)
            {
                return cookies[key].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key">Cookie Key</param>
        /// <param name="value">Cookie值</param>
        public static void Set(HttpWebResponse response, string key, string value, string path = "/")
        {
            Cookie cookie = new Cookie(key, value, path);
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key">Cookie Key</param>
        /// <param name="value">Cookie值</param>
        /// <param name="expirationDateTime">过期时间</param>
        public static void Set(HttpWebResponse response, string key, string value, DateTime? expirationDateTime,  string path = "/")
        {
            Cookie cookie = new Cookie(key, value, path);
            if (expirationDateTime != null && expirationDateTime.HasValue) {
                cookie.Expires = expirationDateTime.Value;
            }
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="key">Cookie Key</param>
        public static void Remove(string key)
        {
            //HttpCookie cookie = HttpContext.Current.Request.CookieCollection[key];
            //if (cookie != null)
            //{
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    HttpContext.Current.Response.CookieCollection.Remove(key);//集合中除不是浏览器真删
            //    HttpContext.Current.Response.CookieCollection.Add(cookie);
            //}
        }

        /// <summary>
        /// 清空Cookie
        /// </summary>
        public static void Clear()
        {
            //int count = HttpContext.Current.Request.CookieCollection.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    HttpCookie hc = HttpContext.Current.Request.CookieCollection[i];
            //    hc.Expires = DateTime.Now.AddDays(-1);
            //    HttpContext.Current.Response.CookieCollection.Remove(hc.Name);
            //    HttpContext.Current.Response.CookieCollection.Add(hc);
            //}
        }
    }
}
