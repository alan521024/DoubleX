namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Reflection;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;


    /// <summary>
    /// 仓储Aop代理钩子
    /// </summary>
    public class RepositoryProxyHook : BaseProxyHook, IProxyGenerationHook
    {
        /// <summary>
        /// 通过生成过程调用来确定指定的方法应被代理。返回给定方法是否需要被代理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override bool ShouldInterceptMethod(Type type, MethodInfo method)
        {
            if (!type.IsBaseFrom<IRepository>())
            {
                return false;
            }
            return base.ShouldInterceptMethod(type, method);
        }
    }
}
