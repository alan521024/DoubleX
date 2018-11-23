namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using System.Reflection;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using AutoMapper.Configuration;

    /// <summary>
    /// 引擎 辅助类
    /// </summary>
    public static class EngineHelper
    {
        private static IEngine singleton;
        private static readonly object syncObject = new object();

        /// <summary>
        /// 运行引擎(私有,用于内部调用)
        /// </summary>
        static IEngine current
        {
            get
            {
                if (singleton == null)
                {
                    lock (syncObject)
                    {
                        if (singleton == null)
                        {
                            singleton = new EngineDefault();
                        }
                    }
                }
                return singleton;
            }
        }

        #region 引擎配置

        /// <summary>
        /// 引擎路径
        /// </summary>
        public static string GlobalPath;

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static EngineConfigModel Configuration
        {
            get
            {
                return current.Config;
            }
        }

        #endregion

        #region 程序启动器

        private static readonly Lazy<Launcher> lazyWorker = new Lazy<Launcher>(() => new Launcher());

        public static Launcher Worker { get { return lazyWorker.Value; } }

        #endregion

        #region 类型管理器

        private static readonly Lazy<ITypeFinder> lazyTypeFinder = new Lazy<ITypeFinder>(() =>
        {
            ITypeFinder _instance = null;
            EnumAppType _appType = Configuration.AppType;

            if (_appType == EnumAppType.Console || _appType == EnumAppType.Service || _appType == EnumAppType.Winfrom || _appType == EnumAppType.Wpf)
            {
                _instance = new AppDomainTypeFinder();
            }
            if (_appType == EnumAppType.Web || _appType == EnumAppType.Api || _appType == EnumAppType.Wap)
            {
                _instance = new WebAppTypeFinder();
            }
            if (_instance == null)
            {
                _instance = new AppDomainTypeFinder();
            }
            return _instance;
        });
        public static ITypeFinder TypeFinder
        {
            get { return lazyTypeFinder.Value; }
        }

        #endregion

        #region 组件管理器

        private static readonly Lazy<ComponentManager> lazyComponentManager = new Lazy<ComponentManager>(() =>
        {
            var mgr = new ComponentManager();
            IEnumerable<Type> types = TypeFinder.FindClassesOfType<IComponentConfiguration>();
            foreach (var item in types)
            {
                mgr.Add(Activator.CreateInstance(item) as IComponentConfiguration);
            }
            return mgr;
        });
        public static ComponentManager Component { get { return lazyComponentManager.Value; } }

        #endregion

        #region 领域管理器

        private static readonly Lazy<DomainProfileManager> lazyDomainProfileManager = new Lazy<DomainProfileManager>(() =>
        {
            var mgr = new DomainProfileManager();
            IEnumerable<Type> types = TypeFinder.FindClassesOfType<IDomainProfile>();
            foreach (var item in types)
            {
                mgr.Add(Activator.CreateInstance(item) as IDomainProfile);
            }
            return mgr;
        });
        public static DomainProfileManager DomainProfile { get { return lazyDomainProfileManager.Value; } }

        #endregion

        #region 容器操作

        #region 注册操作

        /// <summary>
        /// 外部注册(传入核心对象)
        /// </summary>
        /// <param name="action"></param>
        public static void Register(Action<object> action)
        {
            current.IocManager.Register(action);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType<T>()
        {
            current.IocManager.RegisterType<T>();
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType<T>(IocRegisterOptions option)
        {
            current.IocManager.RegisterType<T>(option);
        }

        /// <summary>
        /// 类型服务注入
        /// </summary>
        public static void RegisterType<T, TService>() where TService : class, T
        {
            current.IocManager.RegisterType<T, TService>();
        }

        /// <summary>
        /// 类型服务注入
        /// </summary>
        public static void RegisterType<T, TService>(IocRegisterOptions option) where TService : class, T
        {
            current.IocManager.RegisterType<T, TService>(option);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType(Type type, Type service)
        {
            current.IocManager.RegisterType(type, service);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType(Type type, Type service, IocRegisterOptions option)
        {
            current.IocManager.RegisterType(type, service, option);
        }


        /// <summary>
        /// 程序集注入
        /// </summary>
        public static void RegisterAssembly(Assembly assembly)
        {
            current.IocManager.RegisterAssembly(assembly);
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        public static void RegisterAssembly(Assembly assembly, IocRegisterOptions option)
        {
            current.IocManager.RegisterAssembly(option, assembly);
        }


        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository < >。具体的类型
        /// 将按要求制作，例如，在解析<储存库<int>>() 中。
        /// </summary>
        public static void RegisterGeneric(Type type, Type service)
        {
            current.IocManager.RegisterGeneric(type, service);
        }

        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository < >。具体的类型
        /// 将按要求制作，例如，在解析<储存库<int>>() 中。
        /// </summary>
        public static void RegisterGeneric(Type type, Type service, IocRegisterOptions option)
        {
            current.IocManager.RegisterGeneric(type, service, option);
        }


        #endregion

        #region 解析操作

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return current.IocManager.Resolve<T>();
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Resolve<T>(Type type)
        {
            return (T)current.IocManager.Resolve(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Resolve<T>(params KeyValueModel<string, object>[] parameters)
        {
            return current.IocManager.Resolve<T>(parameters);
        }


        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Resolve<T>(string name, params KeyValueModel<string, object>[] parameters)
        {
            return current.IocManager.Resolve<T>(name, parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Resolve(Type type)
        {
            return current.IocManager.Resolve(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object Resolve(Type type, object parameters)
        {
            return current.IocManager.Resolve(type, parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return current.IocManager.ResolveAll<T>();
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>(object parameters)
        {
            return current.IocManager.ResolveAll<T>(parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            return current.IocManager.ResolveAll(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<object> ResolveAll(Type type, object parameters)
        {
            return current.IocManager.ResolveAll(type, parameters);
        }

        #endregion

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetContainer<T>() where T : class
        {
            return current.IocManager.GetContainer<T>();
        }

        /// <summary>
        /// 容器构建器
        /// </summary>
        public static T GetBuilder<T>() where T : class
        {
            return current.IocManager.GetBuilder<T>();
        }

        /// <summary>
        /// 容器构建
        /// </summary>
        public static T ContainerBuilder<T>() where T : class
        {
            return current.IocManager.ContainerBuilder<T>();
        }

        public static object ContainerBuilder()
        {
            return ContainerBuilder<object>();
        }

        #endregion

        #region 对象映射

        private static readonly AsyncLocal<IMapper> mapper = new AsyncLocal<IMapper>();
        public static void MapperInit()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMissingTypeMaps = true;
            DomainProfile.List.ForEach(x => x.Mapper(cfg));
            var configuration = new MapperConfiguration(cfg);
            mapper.Value = new Mapper(configuration);
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            return mapper.Value.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TEntity Map<TEntity>(object source)
        {
            return mapper.Value.Map<TEntity>(source);
        }

        #endregion

        #region 日志操作

        /// <summary>
        /// 业务日志
        /// </summary>
        public static void LoggingService(string message)
        {
            LoggingManager.GetLogger("Service").Info(message);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        public static void LoggingInfo(string message)
        {
            LoggingManager.GetLogger("Info").Info(message);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        public static void LoggingDebug(string message)
        {
            LoggingManager.GetLogger("Debug").Debug(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static void LoggingError(string message)
        {
            LoggingManager.GetLogger("Error").Error(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static void LoggingError<T>(T obj)
        {
            LoggingManager.GetLogger("Error").Error<T>(obj);
        }

        #endregion
    }
}
