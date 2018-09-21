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
    /// 业务代理钩子（领域业务方法 调用 XXX拦截器）
    /// IProxyGenerationHook接口决定 整个方法 是否运用拦截器，他是在动态构造代理类型的时候使用的；
    /// </summary>
    public class ServiceProxyHook : IProxyGenerationHook
    {
        //调用生成过程来通知整个过程已经完成
        public void MethodsInspected()
        {
            //throw new NotImplementedException();
        }

        //调用的生成过程通知成员没有标记为虚拟。
        public void NonProxyableMemberNotification(Type type, MemberInfo method)
        {
            //throw new NotImplementedException();
        }

        //通过生成过程调用来确定指定的方法应被代理。返回给定方法是否需要被代理
        public bool ShouldInterceptMethod(Type type, MethodInfo method)
        {
            if (!type.IsBaseFrom<IApplicationService>())
            {
                return false;
            }

            if (method.IsSpecialName)
            {
                return false;
            }

            if (TypeHelper.IsIDisposableMethod(method))
            {
                return false;
            }

            if (method.GetBaseDefinition().DeclaringType == typeof(object))
            {
                return false;
            }

            var attr = method.GetCustomAttribute<ServiceAttribute>();
            if (!attr.IsNull() && !attr.IsAop)
            {
                return false;
            }

            Debug.WriteLine(string.Format("ServiceProxyHook-ShouldInterceptMethod: type：{0} | method：{1}", type.FullName, method.Name));

            return true;
        }
    }
}
