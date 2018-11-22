namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using Castle.DynamicProxy;
    using FluentValidation.Validators;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 领域处理
    /// </summary>
    public partial class DomainHelper
    {
        /// <summary>
        /// static ctor
        /// </summary>
        static DomainHelper()
        {
            #region Aop
            //Aop拦截处理列表(Aop/注意处理顺序，应log->input->other...)
            //InterceptorsType(类型)不必须是接口(框架统一使用接口)
            //typeof(ICaptchaVerifyInterceptor), typeof(INotificationInterceptor) 
            //Interceptors = new Type[] { typeof(ILoggingInterceptor), typeof(IInputValidatorInterceptor) };
            #endregion
            
            _repositoryIoc = new IocRegisterOptions()
            {
                InterceptorProxy = new ProxyGenerationOptions()
                {
                    Hook = new RepositoryProxyHook(),
                    Selector = new RepositoryInterceptorSelector()
                },
                InterceptorTypes = new Type[] { typeof(IInputValidatorInterceptor) },
            };

            _serviceIoc = new IocRegisterOptions()
            {
                InterceptorProxy = new ProxyGenerationOptions()
                {
                    Hook = new DomainServiceProxyHook(),
                    Selector = new DomainServiceInterceptorSelector()
                },
                InterceptorTypes = new Type[] { typeof(IInputValidatorInterceptor) }

            };

            _applicationIoc = new IocRegisterOptions()
            {
                InterceptorProxy = new ProxyGenerationOptions()
                {
                    Hook = new ApplicationProxyHook(),
                    Selector = new ApplicationInterceptorSelector()
                },
                InterceptorTypes = new Type[] { typeof(IApplicationLoggingInterceptor) , typeof(IInputValidatorInterceptor) }
            };
        }
        
        /// <summary>
        /// 仓储服务注入
        /// </summary>
        public static IocRegisterOptions RepositoryIoc { get { return _repositoryIoc; } }
        private static IocRegisterOptions _repositoryIoc;

        /// <summary>
        /// 领域服务注入
        /// </summary>
        public static IocRegisterOptions ServiceIoc { get { return _serviceIoc; } }
        private static IocRegisterOptions _serviceIoc;

        /// <summary>
        /// 应用服务注入
        /// </summary>
        public static IocRegisterOptions ApplicationIoc { get { return _applicationIoc; } }
        private static IocRegisterOptions _applicationIoc;

        /// <summary>
        /// 领域业务服务配置初始操作
        /// </summary>
        public static void Configuration(Action<IocRegisterOptions, IocRegisterOptions, IocRegisterOptions> iocAction = null,
            Action<IMapperConfigurationExpression> mapperAction = null)
        {
            //ioc action call
            iocAction?.Invoke(_repositoryIoc, _serviceIoc, _applicationIoc);

            //engine
            EngineConfigModel engine = EngineHelper.Configuration;

            //对象验证
            FluentValidationOptions.Configuration();
            EngineHelper.RegisterType(typeof(IValidatorFactory), typeof(FluentValidatorDefaultFactory));

            //缓存服务
            EngineHelper.RegisterType<ICachingService, RedisCachingService>(new IocRegisterOptions()
            {
                Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.Caching)
                }
            });
            EngineHelper.RegisterType<IAppCachingService, RedisCachingService>(new IocRegisterOptions()
            {
                Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.Caching)
                }
            });

            //配置文件
            //仅支持不需要在IOC注册时获取的配置
            //eg:EngineConfig,...等需要在IOC注册处理,时使用，所以自定义扩展了IConfigObjService
            EngineHelper.RegisterGeneric(typeof(IConfigObjService<>), typeof(DefaultConfigObjService<>),
                new IocRegisterOptions() { InstanceScope = EnumInstanceScope.SingleInstance });

            //Aop(执行日志/输入校验)
            EngineHelper.RegisterType<IApplicationLoggingInterceptor, ApplicationLoggingInterceptor>();
            EngineHelper.RegisterType<IInputValidatorInterceptor, InputValidatorInterceptor>();

            //domain profiles
            var profiles = new List<IDomainProfile>();
            var domainProfiles = EngineHelper.TypeFinder.FindClassesOfType<IDomainProfile>();
            foreach (var item in domainProfiles)
            {
                profiles.Add((Activator.CreateInstance(item) as IDomainProfile));
            }
            profiles.ForEach(x => x.Configuration());

            //mapper
            Mapper.Initialize(config =>
            {
                profiles.ForEach(x => x.Mapper(config));
                mapperAction?.Invoke(config);
            });

            //unitofwork
            EngineHelper.RegisterType<IUnitOfWork, SqlSugarUnitOfWork>();
            EngineHelper.RegisterType<IUnitOfWorkProvider, AsyncUnitOfWorkProvider>(new IocRegisterOptions()
            {
                InstanceScope = EnumInstanceScope.InstancePerLifetimeScope,
                Properties = new List<KeyValueModel<string, object>>() { new KeyValueModel<string, object>("Current", null) }
            });
            EngineHelper.RegisterType<IUnitOfWorkManager, UnitOfWorkManager>();

            //repository
            EngineHelper.RegisterType<IRepository, SqlSugarRepository>(RepositoryIoc);
            EngineHelper.RegisterGeneric(typeof(IRepository<,>), typeof(SqlSugarRepository<,>), RepositoryIoc);
            EngineHelper.RegisterGeneric(typeof(IRepository<>), typeof(SqlSugarRepository<>), RepositoryIoc);

            //domain service
            EngineHelper.RegisterType<IDomainService, DomainService>(ServiceIoc);
            EngineHelper.RegisterGeneric(typeof(IDomainDefaultService<>), typeof(DomainDefaultService<>), ServiceIoc);

            //application service
            EngineHelper.RegisterType<IApplicationService, ApplicationService>(ApplicationIoc);

            //accessor、session、 token、auth
            EngineHelper.RegisterType<IAccessor, DefaultAccessor>();
            EngineHelper.RegisterType<IApplicationSession, DefaultSession>();
            EngineHelper.RegisterType<ITokenService, TokenService>();
            EngineHelper.RegisterType<ITokenStore, TokenStore>(new IocRegisterOptions()
            {
                Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", !engine.Authentication.TokenStore.IsNull()?engine.Authentication.TokenStore:new ConnectionModel())
                }
            });
            EngineHelper.RegisterType<IAuthorizeService, AuthorizationService>();

            //null object
            EngineHelper.RegisterType<ISmsService, NullObjectSmsService>();
        }
    }
}
