using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Guid辅助类
    /// </summary>
    public static class GuidHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取对象GUID
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回GUID对象</returns>
        public static Guid Get(object obj, Guid? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = Guid.Empty;

            if (obj == null)
                return defaultValue.Value;

            Guid returnValue = defaultValue.Value;
            Guid.TryParse(obj.ToString(), out returnValue);
            return returnValue;
        }

        /// <summary>
        /// 获取字符串GUID
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回GUID对象</returns>
        public static Guid Get(string str, Guid? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = Guid.Empty;

            if (string.IsNullOrWhiteSpace(str))
                return defaultValue.Value;

            str = str.Trim();

            Guid returnValue = defaultValue.Value;
            Guid.TryParse(str, out returnValue);
            return returnValue;
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取GUID的字符串
        /// </summary>
        public static string FormatString(this Guid guid, bool removeSplit = false, bool isCaseUpper = false)
        {
            string guidStr = removeSplit ? guid.ToString("N") : guid.ToString();
            return isCaseUpper ? guidStr.ToUpper() : guidStr.ToLower();
        }


        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取GUID的字符串
        /// </summary>
        public static string GetToString(Guid? guid, bool removeSplit = false, bool isCaseUpper = false)
        {
            if (!guid.HasValue)
                return "";
            string guidStr = removeSplit ? guid.Value.ToString("N") : guid.Value.ToString();
            return isCaseUpper ? guidStr.ToUpper() : guidStr.ToLower();
        }

        #endregion
    }
}
