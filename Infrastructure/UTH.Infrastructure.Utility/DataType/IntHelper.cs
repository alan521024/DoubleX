using System;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 整数辅助类
    /// </summary>
    public static class IntHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取对象整数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回int对象</returns>
        public static int Get(object obj, int defaultValue = 0)
        {
            if (obj == null)
                return defaultValue;
            int returnValue = defaultValue;

            //传入decimail 如1 ToString 后变成了1.00 int.TryParse报错
            try
            {
                returnValue = Convert.ToInt32(obj);
            }
            catch(Exception ex)
            {
                int.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// 获取字符串整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回int对象</returns>
        public static int Get(string str, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(str))
                return defaultValue;

            str = str.ToLower().Trim();
            if (str == "true" || str == "false")
            {
                return str == "true" ? 1 : 0;
            }

            int returnValue = defaultValue;
            int.TryParse(str, out returnValue);
            return returnValue;
        }

        /// <summary>
        /// 获取bool整数
        /// </summary>
        /// <param name="obj">bool 对象</param>
        /// <returns>返回int对象</returns>
        public static int Get(bool obj)
        {
            return obj ? 1 : 0;
        }

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion
    }
}
