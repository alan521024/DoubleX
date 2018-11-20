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
    /// 领域处理
    /// </summary>
    public static class Domains
    {
        /// <summary>
        /// 领域业务服务配置初始操作
        /// </summary>
        public static void Initialize(Action<IocRegisterOptions, IocRegisterOptions, IocRegisterOptions> iocAction,Action<IMapperConfigurationExpression> mapperAction)
        {
            //default value
            IocRegisterOptions serviceRegOptions = ServiceOptions.DefaultIoc;

            //iocAction?.Invoke(serviceRegOptions);

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

            //配置文件
            //仅支持不需要在IOC注册时获取的配置
            //eg:EngineConfig,...等需要在IOC注册处理,时使用，所以自定义扩展了IConfigObjService
            EngineHelper.RegisterGeneric(typeof(IConfigObjService<>), typeof(DefaultConfigObjService<>),
                new IocRegisterOptions() { InstanceScope = EnumInstanceScope.SingleInstance });

            //Aop(执行日志/输入校验)
            EngineHelper.RegisterType<ILoggingInterceptor, LoggingInterceptor>();
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

            //domain repository
            EngineHelper.RegisterType<IRepository, SqlSugarRepository>(RepositoryOptions.Default);
            EngineHelper.RegisterGeneric(typeof(IRepository<,>), typeof(SqlSugarRepository<,>), RepositoryOptions.Default);
            EngineHelper.RegisterGeneric(typeof(IRepository<>), typeof(SqlSugarRepository<>), RepositoryOptions.Default);

            //domain service
            EngineHelper.RegisterType<IDomainService, DomainService>(ServiceOptions.DefaultIoc);
            EngineHelper.RegisterGeneric(typeof(IDomainDefaultService<>), typeof(DomainDefaultService<>), serviceRegOptions);

            //application service
            EngineHelper.RegisterType<IApplicationService, ApplicationService>(ApplicationOptions.Default);

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
        }
    }
}
