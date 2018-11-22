namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Diagnostics;
    using Castle.DynamicProxy;
    using Microsoft.AspNetCore.Mvc;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Web Action Aop 代理钩子
    /// </summary>
    public class ActionProxyHook : IProxyGenerationHook
    {
        /// <summary>
        /// 调用生成过程来通知整个过程已经完成
        /// </summary>
        public void MethodsInspected()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 调用的生成过程通知成员没有标记为虚拟。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        public void NonProxyableMemberNotification(Type type, MemberInfo method)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 通过生成过程调用来确定指定的方法应被代理。返回给定方法是否需要被代理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo method)
        {
            if (AspNetCoreMvcHelper.IsAction(type, method))
            {
                Debug.WriteLine(string.Format("Intercept(Action): type：{0} | method：{1}", type.FullName, method.Name));
                return true;
            }

            if (DynamicApiHelper.IsServiceAction(type, method))
            {
                Debug.WriteLine(string.Format("Intercept(DynamicApi): type：{0} | method：{1}", type.FullName, method.Name));
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
