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
    /// 应用服务Aop代理钩子
    /// </summary>
    public class ApplicationProxyHook : BaseProxyHook, IProxyGenerationHook
    {
        /// <summary>
        /// 通过生成过程调用来确定指定的方法应被代理。返回给定方法是否需要被代理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override bool ShouldInterceptMethod(Type type, MethodInfo method)
        {
            if (!type.IsBaseFrom<IApplicationService>())
            {
                return false;
            }

            if (!base.ShouldInterceptMethod(type, method))
            {
                return false;
            }

            Debug.WriteLine(string.Format("Intercept(Application): type：{0} | method：{1}", type.FullName, method.Name));

            return true;
        }
    }
}
