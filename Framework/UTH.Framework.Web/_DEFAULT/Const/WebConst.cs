using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// Web默认值
    /// </summary>
    internal class WebConst
    {
        /// <summary>
        /// 来源页
        /// </summary>
        public const string ReturnUrl = "ReturnUrl";

        /// <summary>
        /// 类型列表
        /// </summary>
        public static class TypeAndDefault
        {
            public static ResultWrapAttribute DefaultResultWrapAttribute = new ResultWrapAttribute();
        }

        /// <summary>
        /// 认证
        /// </summary>
        public static class Authentication
        {
            public const string Scheme = "UTHAuthentication";
        }

        /// <summary>
        /// 消息
        /// </summary>
        public static class Message
        {

        }

        /// <summary>
        /// 缓存
        /// </summary>
        public static class Cache
        {

        }
    }
}
