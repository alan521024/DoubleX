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
        public DomainOptions()
        {
            var config = EngineHelper.Configuration;

            //Aop拦截处理列表(Aop/注意处理顺序，应log->input->other...)
            //InterceptorsType(类型)不必须是接口(框架统一使用接口)
            //typeof(ICaptchaVerifyInterceptor), typeof(INotificationInterceptor) 
            Interceptors = new Type[] { typeof(ILoggingInterceptor), typeof(IInputValidatorInterceptor) };

            //仓储类型
            Repositorys = new List<KeyValueModel<Type, Type>>();

            //IOC注册
            var repParams = new List<KeyValueModel<string, object>>();
            repParams.Add(new KeyValueModel<string, object>("connectionStr", null));
            if (!config.Store.Database.IsNull())
            {
                repParams.Add(new KeyValueModel<string, object>("connectionModel", new ConnectionModel(config.Store.Database.GetConnectionString())
                {
                    DbType = config.Store.Database.DbType
                }));
            }
            repParams.Add(new KeyValueModel<string, object>("connectionClient", null));
            repParams.Add(new KeyValueModel<string, object>("session", null));

            IocRepositoryOption = new IocRegisterOptions() { Parameters = repParams };

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
        /// 仓储类型
        /// </summary>
        public List<KeyValueModel<Type, Type>> Repositorys { get; set; } = new List<KeyValueModel<Type, Type>>();

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
