namespace UTH.Domain
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
    using UTH.Framework;

    /// <summary>
    /// 应用服务配置
    /// </summary>
    public class ApplicationOptions
    {
        /// <summary>
        /// 服务拦截器(Aop)
        /// </summary>
        public static Type[] Interceptors { get; }

        /// <summary>
        /// 服务默认注入
        /// </summary>
        public static IocRegisterOptions Default { get; set; }
    }
}
