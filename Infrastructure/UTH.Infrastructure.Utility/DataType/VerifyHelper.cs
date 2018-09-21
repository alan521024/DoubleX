using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 信息校验辅助类
    /// </summary>
    public static class VerifyHelper
    {
        #region IsNull

        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断对象是否为空(扩展方式)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj, params string[] param)
        {
            return IsNull(obj);
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// 判断对象是否Null或空(string null/"",list null/0,arr null/0)
        /// </summary>
        public static bool IsEmpty(object obj)
        {
            if (obj == null)
                return true;

            string objType = obj.GetType().FullName;

            if (objType == "System.String" || objType == "Microsoft.Extensions.Primitives.StringValues")
            {
                return string.IsNullOrWhiteSpace(obj.ToString()) ? true : false;
            }
            else if (objType == "System.DateTime")
            {
                DateTime date = DateTime.MinValue;
                DateTime.TryParse(obj.ToString(), out date);
                return date == DateTime.MinValue || DateTimeHelper.GetEnd(DateTime.MinValue) == date;
            }
            else if (objType == "System.Guid")
            {
                Guid value = Guid.Empty;
                Guid.TryParse(obj.ToString(), out value);
                return value == Guid.Empty;
            }
            else if (objType == "Newtonsoft.Json.Linq.JValue")
            {
                return (obj as JArray).Count == 0;
            }
            else if (objType == "Newtonsoft.Json.Linq.JObject")
            {
                return (obj as JObject).Properties().Count() == 0;
            }
            else if (objType == "System.Int32")
            {
                return IntHelper.Get(obj) == 0;
            }
            else if (obj is ICollection && (obj as ICollection).Count == 0)
            {
                return true;
            }
            //0001/1/1 0:00:00

            //System.Web.HttpValueCollection(NameValueCollection)
            //Newtonsoft.Json.Linq.JArray
            return string.IsNullOrWhiteSpace(obj.ToString());
        }
        /// <summary>
        /// 判断对象是否Null或空(string null/"",list null/0,arr null/0)
        /// </summary>
        public static bool IsEmpty(this object obj, params string[] param)
        {
            return IsEmpty(obj);
        }


        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(this string obj, params string[] param)
        {
            return IsEmpty(obj);
        }


        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(StringBuilder build)
        {
            return build == null || (build != null && build.Length == 0);
        }
        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(this StringBuilder build, params string[] param)
        {
            return IsEmpty(build);
        }


        /// <summary>
        /// 判断是否空Guid（NULL Empty）
        /// </summary>
        public static bool IsEmpty(Guid guid)
        {
            return guid == null || guid == Guid.Empty;
        }
        /// <summary>
        /// 判断是否空Guid（NULL Empty）
        /// </summary>
        public static bool IsEmpty(this Guid guid, params string[] param)
        {
            return IsEmpty(guid);
        }


        /// <summary>
        /// 判断是否空DateTime（NULL or MinValue）
        /// </summary>
        public static bool IsEmpty(DateTime dateTime)
        {
            return dateTime == null || dateTime == DateTime.MinValue;
        }
        /// <summary>
        /// 判断是否空DateTime（NULL Empty）
        /// </summary>
        public static bool IsEmpty(this DateTime dateTime, params string[] param)
        {
            return IsEmpty(dateTime);
        }


        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsEmpty(ICollection list)
        {
            return !(list != null && list.Count > 0);
        }
        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsEmpty(this ICollection list, params string[] param)
        {
            return IsEmpty(list);
        }

        #endregion

        #region CheckNull or Empty And Throw ArgumentNullException

        /// <summary>
        /// CheckNull throw ArgumentNullException
        /// </summary>
        public static void CheckNull(this object obj, params string[] param)
        {
            if (obj.IsNull())
                throw new ArgumentNullException("obj is null");
        }

        /// <summary>
        /// CheckEmpty throw ArgumentNullException
        /// </summary>
        public static void CheckEmpty(this object obj, params string[] param)
        {
            if (obj.IsEmpty())
                throw new ArgumentNullException("obj is empty");
        }

        #endregion

        #region Web相关验证判断

        ///// <summary>
        ///// 判断是否支持Context
        ///// </summary>
        //public static bool IsSupperHttpContext()
        //{
        //    //上下文无效(多线程，当前为非请求线程 )
        //    return HttpContext.Current != null;
        //}

        ///// <summary>
        ///// 判断是否支持Cookie
        ///// </summary>
        ///// <returns></returns>
        //public static bool IsSupperHttpCookie()
        //{
        //    //上下文无效(多线程，当前为非请求线程 )
        //    var context = HttpContext.Current;
        //    if (context == null)
        //        return false;

        //    //判断是否有Cookies(Golable Start 无Cookies)
        //    return context.Request.CookieCollection != null;
        //}

        ///// <summary>
        ///// 判断是否Ajax请求(WebForm)
        ///// </summary>
        //public static bool IsAjax(HttpContext context)
        //{
        //    if (context == null)
        //        return false;
        //    return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //}

        ///// <summary>
        ///// 判断是否Ajax请求(Mvc)
        ///// </summary>
        //public static bool IsAjax(HttpContext context)
        //{
        //    if (context != null)
        //    {
        //        return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //    }
        //    return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

        //}

        #endregion
    }
}
