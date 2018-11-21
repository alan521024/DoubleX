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
    /// Aop代理钩子（是否调用，或调用什么样的拦截器）
    /// IProxyGenerationHook接口决定 整个方法 是否运用拦截器，他是在动态构造代理类型的时候使用的；
    /// 抽象基类(默认实体，子类调用)
    /// </summary>
    public abstract class BaseProxyHook : IProxyGenerationHook
    {
        /// <summary>
        /// 调用生成过程来通知整个过程已经完成
        /// </summary>
        public virtual void MethodsInspected()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 调用的生成过程通知成员没有标记为虚拟。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        public virtual void NonProxyableMemberNotification(Type type, MemberInfo method)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 通过生成过程调用来确定指定的方法应被代理。返回给定方法是否需要被代理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public virtual bool ShouldInterceptMethod(Type type, MethodInfo method)
        {
            //Debug.WriteLine(string.Format("Intercept(BaseProxyHook): type：{0} | method：{1}", type.FullName, method.Name));

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

            return true;
        }
    }
}
