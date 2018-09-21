using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Url 辅助操作类
    /// </summary>
    public static class UrlsHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 设置Url Query 参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string SetQuerys(this string url, params KeyValueModel[] param)
        {
            string root = "", queryStr = "";
            if (url.IsEmpty())
            {
                url = "";
            }

            var i = url.IndexOf('?');
            var isUrl = StringHelper.IsUrl(url);
            if (i > -1)
            {
                root = url.Substring(0, url.IndexOf('?'));
                queryStr = i > -1 ? url.Substring(i + 1) : "";
            }
            else
            {
                root = url;
                //if (isUrl)
                //{
                //}
                //else
                //{
                //    queryStr = url;
                //}
            }

            List<KeyValueModel> querys = StringHelper.GetToListKeyValue(queryStr, listSplit: '&');
            if (!param.IsEmpty())
            {
                foreach (var item in param)
                {
                    var queryModel = querys.Where(x => x.Key == item.Key).FirstOrDefault();
                    if (queryModel.IsNull())
                    {
                        querys.Add(item);
                    }
                    else {
                        queryModel.Value = item.Value;
                    }
                }
            }

            StringBuilder queryBuilder = new StringBuilder();
            foreach (var item in querys)
            {
                queryBuilder.AppendFormat("&{0}={1}", item.Key, item.Value);
            }
            queryBuilder = queryBuilder.TrimStart('&');

            if (root.IsEmpty() && queryStr.IsEmpty())
            {
                return queryBuilder.ToString();
            }
            else
            {
                return string.Format("{0}{1}{2}", root, (isUrl ? "?" : ""), queryBuilder.ToString());
            }
        }

        #endregion

    }
}
