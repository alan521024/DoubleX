using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// 授权认证异常
    /// </summary>
    public class AuthorizeException : DbxException
    {
        public AuthorizeException(EnumCode code) : base(code)
        {
            Code = code;
        } 
        public AuthorizeException(EnumCode code, string message) : base(code, message)
        {
            Code = code;
        }
        public AuthorizeException(EnumCode code, Exception exception) : base(code, exception)
        {
            Code = code;
        }
        public AuthorizeException(EnumCode code, Exception exception, string message) : base(code, exception, message)
        {
            Code = code;
        }

        /// <summary>
        /// 参数/返回/配置式异常,
        /// code == EnumCode.参数异常 || code == EnumCode.配置异常 || code == EnumCode.网络异常 || code == EnumCode.返回异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        public AuthorizeException(EnumCode code, params string[] args) : base(code, args)
        {
            Code = code;
        }

        /// <summary>
        /// 异常Code
        /// </summary>
        public override EnumCode Code { get; set; } = EnumCode.未知异常;
    }
}
