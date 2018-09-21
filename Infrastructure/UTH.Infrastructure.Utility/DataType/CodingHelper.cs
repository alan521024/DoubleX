using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 编码辅助类
    /// </summary>
    public static class CodingHelper
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
        /// 获取 Byte[] (从Base64)
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Byte[] GetByteByBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return null;

            return Convert.FromBase64String(base64);
        }


        /// <summary>
        /// 获取 Base64 (从Byte[])
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string GetBase64ByByte(byte[] bytes)
        {
            if (bytes.IsEmpty())
            {
                return "";
            }
            return Convert.ToBase64String(bytes);
        }

        public static string UrlEncoding(string str)
        {
            return System.Net.WebUtility.UrlEncode(str);
        }

        public static string UrlDecode(string str)
        {
            return System.Net.WebUtility.UrlDecode(str);
        }

        #endregion
    }
}
