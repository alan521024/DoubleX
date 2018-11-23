using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using Castle.DynamicProxy;
using AutoMapper;
using FluentValidation;

namespace UTH.Framework
{
    /// <summary>
    /// 领域操作辅助类
    /// </summary>
    public static class FrameworkHelper
    {
        ///// <summary>
        ///// 方法是否输入校验
        ///// </summary>
        //public static bool IsValidatorMethod(Type type, MethodInfo method)
        //{
        //    var methodParams = method.GetParameters();
        //    if (!methodParams.IsEmpty())
        //    {
        //        foreach (var item in methodParams)
        //        {
        //            if (item.ParameterType.IsBaseFrom<IInput>())
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 方法是否含验证码
        ///// </summary>
        //public static bool IsCaptchaMethod(Type type, MethodInfo method)
        //{
        //    return !method.GetCustomAttributes<CaptchaAttribute>().IsEmpty();
        //}

        ///// <summary>
        ///// 是否为消息通知
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static bool IsNotification(Type type, MethodInfo method)
        //{
        //    var attrs = method.GetCustomAttributes<NotificationAttribute>();
        //    var attrs2 = method.GetCustomAttributes<NotificationAttribute>(false);
        //    return !method.GetCustomAttributes<NotificationAttribute>().IsEmpty();
        //}

        //public static TService CreateTransactionService<TService, TEntity>(IRepository repository)
        //    where TService : IApplicationService
        //    where TEntity : class, IEntity
        //{
        //    var repParams = new KeyValueModel<string, object>("connectionClient", repository.GetClient());
        //    var repObj = EngineHelper.Resolve<IRepository<TEntity>>(repParams);
        //    return EngineHelper.Resolve<TService>(new KeyValueModel<string, object>("_repository", repObj));
        //}
        
        /// <summary>
        /// 判断方法是否勾子
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsMethodHook(Type type, MethodInfo method)
        {

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

        /// <summary>
        /// 获取服务拦截器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public static IInterceptor[] GetServiceInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (!interceptors.IsEmpty())
            {
                return interceptors.Where(i => i is IServiceInterceptor).ToArray();
            }
            return interceptors;
        }

        /// <summary>
        /// 验证规则第一个消息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string VerifyFirstMessage(this ValidationResult result)
        {
            result.CheckNull();
            return result.ToString("|").Split('|').FirstOrDefault();
        }

        /// <summary>
        /// 获取输入校验异常
        /// </summary>
        public static DbxException GeValidationException(ValidationResult result)
        {
            result.CheckNull();

            List<KeyValueModel> errors = new List<KeyValueModel>();
            if (!result.IsNull() && !result.Errors.IsEmpty())
            {
                foreach (var item in result.Errors)
                {
                    if (!errors.Any(x => x.Key == item.PropertyName))
                    {
                        errors.Add(new KeyValueModel(item.PropertyName, item.ErrorMessage));
                    }
                    else
                    {
                        //如果己存在，取最后一条错误消息
                        errors.FirstOrDefault(x => x.Key == item.PropertyName).Value = item.ErrorMessage;
                    }
                }
            }
            return new DbxException(EnumCode.校验失败, StringHelper.Get(errors.Select(x => x.Value).ToArray(), "|"));
        }
    }

}
