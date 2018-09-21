namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Asp.net Core Mvc 辅助操作
    /// </summary>
    public static class AspNetCoreMvcHelper
    {
        /// <summary>
        /// Web扩展默认属性
        /// </summary>
        public static WebExtendAttribute DefaultWebExtendAttribute = new WebExtendAttribute();

        #region Action

        /// <summary>
        /// 判断是否Action
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool IsAction(Type typeInfo, MethodInfo methodInfo)
        {
            if (typeInfo == null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            // The SpecialName bit is set to flag members that are treated in a special way by some compilers
            // (such as property accessors and operator overloading methods).
            if (methodInfo.IsSpecialName)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(NonActionAttribute)))
            {
                return false;
            }

            // Overridden methods from Object class, e.g. Equals(Object), GetHashCode(), etc., are not valid.
            if (methodInfo.GetBaseDefinition().DeclaringType == typeof(object))
            {
                return false;
            }

            // Dispose method implemented from IDisposable is not valid
            if (TypeHelper.IsIDisposableMethod(methodInfo))
            {
                return false;
            }

            if (methodInfo.IsStatic)
            {
                return false;
            }

            if (methodInfo.IsAbstract)
            {
                return false;
            }

            if (methodInfo.IsConstructor)
            {
                return false;
            }

            if (methodInfo.IsGenericMethod)
            {
                return false;
            }

            return methodInfo.IsPublic;
        }

        #endregion
    }
}
