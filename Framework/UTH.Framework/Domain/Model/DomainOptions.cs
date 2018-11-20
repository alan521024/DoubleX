namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;
    using AutoMapper;
    using FluentValidation;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 领域配置
    /// </summary>
    public class DomainOptions
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DomainOptions()
        {
            var config = EngineHelper.Configuration;

            //Aop拦截处理列表(Aop/注意处理顺序，应log->input->other...)
            //InterceptorsType(类型)不必须是接口(框架统一使用接口)
            //typeof(ICaptchaVerifyInterceptor), typeof(INotificationInterceptor) 
            Interceptors = new Type[] { typeof(ILoggingInterceptor), typeof(IInputValidatorInterceptor) };

            IocRepositoryOption = new IocRegisterOptions();

            IocServiceOption = new IocRegisterOptions()
            {
                InterceptorProxy = new ProxyGenerationOptions()
                {
                    Hook = new ServiceProxyHook(),
                    Selector = new ServiceInterceptorSelector()
                },
                InterceptorTypes = Interceptors
            };

        }

        /// <summary>
        /// 服务拦截器(Aop)
        /// </summary>
        public Type[] Interceptors { get; }

        /// <summary>
        /// 仓储配置
        /// </summary>
        public IocRegisterOptions IocRepositoryOption { get; }

        /// <summary>
        /// 服务配置
        /// </summary>
        public IocRegisterOptions IocServiceOption { get; }

        /// <summary>
        /// 对象映射
        /// </summary>
        public Action<IMapperConfigurationExpression> Mapper { get; set; }

    }
}
