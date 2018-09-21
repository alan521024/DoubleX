namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 逻辑类型辅助类
    /// </summary>
    public static class BoolHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取对象Bool值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回bool对象</returns>
        public static bool Get(object obj, bool defaultValue = false)
        {
            if (obj == null)
                return defaultValue;

            string str = obj.ToString().ToLower().Trim();

            if (str == "yes" || str == "true" || str == "是" || str == "正确" || str == "1")
                return true;

            if (str == "no" || str == "false" || str == "否" || str == "错误" || str == "0")
                return false;

            return defaultValue;
        }

        /// <summary>
        /// 获取字符串Bool值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">"false"</param>
        /// <returns></returns>
        public static bool Get(string value, string defaultValue = "false")
        {
            if (string.IsNullOrWhiteSpace(value))
                value = defaultValue;
            value = value.ToLower().Trim();
            return value == "true" || value == "1" ? true : false;
        }

        /// <summary>
        /// 获取整数Bool值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns></returns>
        public static bool Get(int value, int defaultValue = 0)
        {
            if (value == 0)
                return false;
            return true;
        }

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取可空类型值(为空时返回默认)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetByNullType(bool? value, bool defaultNullValue = false)
        {
            if (value == null)
                return defaultNullValue;

            return value.Value;
        }

        #endregion
    }
}
