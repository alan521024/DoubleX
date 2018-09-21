using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 长整数工具类
    /// </summary>
    public static class LongHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取对象长整型整数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回long对象</returns>
        public static long Get(object value, long defaultValue = 0)
        {
            if (value == null)
                return defaultValue;
            long returnValue = defaultValue;
            long.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }

        /// <summary>
        /// 获取字符串长整型整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回long对象</returns>
        public static long Get(string str, long defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(str))
                return defaultValue;

            str = str.Trim();

            long returnValue = defaultValue;
            long.TryParse(str, out returnValue);
            return returnValue;
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 保留小数位数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static decimal FormatFixed(this decimal value, int precision = 2)
        {
            Math.Round(0.333333, 2);//按照四舍五入的国际标准

            decimal result = Math.Round(value, precision);
            if (result > 999999999999)
            {
                result = 999999999999;
            }
            return result;
        }

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion
    }
}
