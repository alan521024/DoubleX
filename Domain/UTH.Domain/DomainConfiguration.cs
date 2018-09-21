namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using FluentValidation.Validators;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 领域业务配置
    /// </summary>
    public static class DomainConfiguration
    {
        /// <summary>
        /// 领域默认配置
        /// </summary>
        public static DomainOptions Options { get; set; } = new DomainOptions();

        /// <summary>
        /// 领域业务服务配置初始操作
        /// </summary>
        public static void Initialize(Action<DomainOptions> action)
        {
            //options
            action?.Invoke(Options);

            //engine
            EngineConfigModel engine = EngineHelper.Configuration;

            //对象验证
            FluentValidationOptions.Configuration();
            EngineHelper.RegisterType(typeof(IValidatorFactory), typeof(FluentValidatorDefaultFactory));

            //配置文件
            //仅支持不需要在IOC注册时获取的配置
            //eg:EngineConfig,...等需要在IOC注册处理,时使用，所以自定义扩展了IConfigObjService
            EngineHelper.RegisterGeneric(typeof(IConfigObjService<>), typeof(DefaultConfigObjService<>), new IocRegisterOptions() { SingleInstance = true });

            //缓存服务
            if (!engine.Store.DefaultCache.IsNull())
            {
                EngineHelper.RegisterType<ICachingService, RedisCachingService>(new IocRegisterOptions()
                {
                    Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.DefaultCache)
                }
                });
            }
            if (!engine.Store.AppCache.IsNull())
            {
                EngineHelper.RegisterType<IAppCachingService, AppCachingService>(new IocRegisterOptions()
                {
                    Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.AppCache)
                }
                });
            }
            if (!engine.Store.DataCache.IsNull())
            {
                EngineHelper.RegisterType<IDataCachingService, DataCachingService>(new IocRegisterOptions()
                {
                    Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.DataCache)
                }
                });
            }
            if (!engine.Store.SessionCache.IsNull())
            {
                EngineHelper.RegisterType<ISessionCachingService, SessionCachingService>(new IocRegisterOptions()
                {
                    Parameters = new List<KeyValueModel<string, object>>(){
                    new KeyValueModel<string, object>("model", engine.Store.SessionCache)
                }
                });
            }

            //Aop(执行日志/输入校验)
            EngineHelper.RegisterType<ILoggingInterceptor, LoggingInterceptor>();
            EngineHelper.RegisterType<IInputValidatorInterceptor, InputValidatorInterceptor>();

            //domain profiles
            var profiles = new List<IDomainProfile>();
            var items = EngineHelper.TypeFinder.FindClassesOfType<IDomainProfile>();
            foreach (var item in EngineHelper.TypeFinder.FindClassesOfType<IDomainProfile>())
            {
                profiles.Add((Activator.CreateInstance(item) as IDomainProfile));
            }

            //domain configuration
            profiles.ForEach(x => x.Configuration());

            //domain mapper
            Mapper.Initialize(config =>
            {
                profiles.ForEach(x => x.Mapper(config));
                Options.Mapper?.Invoke(config);
            });

            //仓储、业务
            Options.Repositorys.ForEach((x) =>
            {
                EngineHelper.RegisterGeneric(x.Key, x.Value, Options.IocRepositoryOption);
            });
            EngineHelper.RegisterType<IApplicationService, ApplicationService>(Options.IocServiceOption);
            EngineHelper.RegisterGeneric(typeof(IApplicationDefault<>), typeof(ApplicationDefault<>), Options.IocServiceOption);

            //访问者、Session、 Token、授权认证
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
        }
    }
}
