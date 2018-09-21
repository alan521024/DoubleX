namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// UTH异常
    /// </summary>
    public class DbxException : Exception
    {
        #region 类属性(EnumCode)

        /// <summary>
        /// 异常Code
        /// </summary>
        public virtual EnumCode Code { get; set; } = EnumCode.未知异常;

        #endregion

        #region 构造方法

        public DbxException(EnumCode code) : base(code.ToString())
        {
            Code = code;
        }
        public DbxException(EnumCode code, string message) : base(message)
        {
            Code = code;
        }
        public DbxException(EnumCode code, Exception exception) : base(code.ToString(), exception)
        {
            Code = code;
        }
        public DbxException(EnumCode code, Exception exception, string message) : base(message, exception)
        {
            Code = code;
        }

        /// <summary>
        /// 参数/返回/配置式异常,
        /// code == EnumCode.参数异常 || code == EnumCode.配置异常 || code == EnumCode.网络异常 || code == EnumCode.返回异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        public DbxException(EnumCode code, params string[] args) : base(string.Join(",", args))
        {
            Code = code;
        }

        #endregion
    }
}
