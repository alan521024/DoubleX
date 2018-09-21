//using System;
//using System.Collections.Generic;
//using System.Text;
//using Castle.MicroKernel.Registration;
//using Castle.Windsor;
//using Castle.Windsor.Installer;
//using UTH.Infrastructure.Utility;
//using System.Reflection;
//using System.Linq;

//namespace UTH.Framework
//{
//    ///// <summary>
//    ///// ICO容器 管理器
//    ///// </summary>
//    //public class CastleIocManager : IIocManager
//    //{
//    //    /// <summary>
//    //    /// IOC容器管理器单例
//    //    /// </summary>
//    //    public static CastleIocManager Instance { get; private set; }

//    //    /// <summary>
//    //    /// IOC容器
//    //    /// </summary>
//    //    protected IWindsorContainer IocContainer { get; private set; }

//    //    /// <summary>
//    //    /// 注册列表
//    //    /// </summary>
//    //    private readonly List<IIocDependencyRegistrar> _registrars;

//    //    static CastleIocManager()
//    //    {
//    //        Instance = new CastleIocManager();
//    //    }

//    //    public CastleIocManager()
//    //    {
//    //        IocContainer = new WindsorContainer();
//    //        _registrars = new List<IIocDependencyRegistrar>();

//    //        //Register self!
//    //        //IocContainer.Register(Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this) );
//    //    }

//    //    #region 注入操作

//    //    /// <summary>
//    //    /// 程序集注入
//    //    /// </summary>
//    //    public void RegisterAssembly(Assembly assembly, params Dependency[] dependencies)
//    //    {
//    //        RegisterAssembly(assembly, new IocRegisterOptions(), dependencies);
//    //    }

//    //    /// <summary>
//    //    /// 程序集注入
//    //    /// </summary>
//    //    public void RegisterAssembly(Assembly assembly, IocRegisterOptions option, params Dependency[] dependencies)
//    //    {
//    //        var context = new IocDependencyRegistrarContext(assembly, this, option);

//    //        foreach (var registerer in _registrars)
//    //        {
//    //            registerer.RegisterAssembly(context);
//    //        }

//    //        if (option.InstallInstallers)
//    //        {
//    //            IocContainer.Install(FromAssembly.Instance(assembly));
//    //        }
//    //    }

//    //    /// <summary>
//    //    /// 类型注入
//    //    /// </summary>
//    //    public void RegisterType<T>(params Dependency[] dependencies) where T : class
//    //    {
//    //        var registration = Component.For<T>();
//    //        if (dependencies != null && dependencies.Length > 0)
//    //        {
//    //            registration = registration.DependsOn(dependencies);
//    //        }
//    //        IocContainer.Register(registration);
//    //    }

//    //    /// <summary>
//    //    /// 类型注入
//    //    /// </summary>
//    //    public void RegisterType<T>(IocRegisterOptions option, params Dependency[] dependencies) where T : class
//    //    {
//    //        var registration = Component.For<T>();
//    //        if (dependencies != null && dependencies.Length > 0)
//    //        {
//    //            registration = registration.DependsOn(dependencies);
//    //        }
//    //        IocContainer.Register(registration);
//    //    }

//    //    /// <summary>
//    //    /// 类型注入
//    //    /// </summary>
//    //    public void RegisterType<T, TService>(params Dependency[] dependencies) where T : class where TService : class, T
//    //    {
//    //        var registration =Component.For<T>().ImplementedBy<TService>();
//    //        if (dependencies != null && dependencies.Length > 0)
//    //        {
//    //            registration = registration.DependsOn(dependencies);
//    //        }
//    //        IocContainer.Register(registration);
//    //    }

//    //    /// <summary>
//    //    /// 类型注入
//    //    /// </summary>
//    //    public void RegisterType<T, TService>(IocRegisterOptions option, params Dependency[] dependencies) where T : class where TService : class, T
//    //    {
//    //        var registration = Component.For<T>().ImplementedBy<TService>();
//    //        if (dependencies != null && dependencies.Length > 0)
//    //        {
//    //            registration = registration.DependsOn(dependencies);
//    //        }
//    //        IocContainer.Register(registration);
//    //    }

//    //    /// <summary>
//    //    /// 类型注入
//    //    /// </summary>
//    //    public void RegisterType(Type type, Type service, params Dependency[] dependencies)
//    //    {
//    //        var registration = Component.For(type).ImplementedBy(service);
//    //        if (dependencies != null && dependencies.Length > 0)
//    //        {
//    //            registration = registration.DependsOn(dependencies);
//    //        }
//    //        IocContainer.Register(registration);
//    //    }

//    //    //参考资料：
//    //    //ABP源码
//    //    //https://www.cnblogs.com/genson/archive/2009/12/09/1620494.html
//    //    //注入带参构造函数三种种参数注入方法：http://www.jianshu.com/p/d21c29334f78
//    //    //Castle one by one 注入: https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
//    //    //Castle 生命周期： http://terrylee.cnblogs.com/archive/2006/04/26/385127.html

//    //    #endregion

//    //    #region 解析操作

//    //    public T Resolve<T>()
//    //    {
//    //        return IocContainer.Resolve<T>();
//    //    }

//    //    public T Resolve<T>(Type type)
//    //    {
//    //        return (T)IocContainer.Resolve(type);
//    //    }

//    //    public T Resolve<T>(object argumentsAsAnonymousType)
//    //    {
//    //        return IocContainer.Resolve<T>(argumentsAsAnonymousType);
//    //    }

//    //    public object Resolve(Type type)
//    //    {
//    //        return IocContainer.Resolve(type);
//    //    }

//    //    public object Resolve(Type type, object argumentsAsAnonymousType)
//    //    {
//    //        return IocContainer.Resolve(type, argumentsAsAnonymousType);
//    //    }

//    //    public T[] ResolveAll<T>()
//    //    {
//    //        return IocContainer.ResolveAll<T>();
//    //    }

//    //    public T[] ResolveAll<T>(object argumentsAsAnonymousType)
//    //    {
//    //        return IocContainer.ResolveAll<T>(argumentsAsAnonymousType);
//    //    }

//    //    public object[] ResolveAll(Type type)
//    //    {
//    //        return IocContainer.ResolveAll(type).Cast<object>().ToArray();
//    //    }

//    //    public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
//    //    {
//    //        return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
//    //    }

//    //    public void Release(object obj)
//    //    {
//    //        IocContainer.Release(obj);
//    //    }

//    //    #endregion

//    //    /// <summary>
//    //    /// 增加注入注册器信息
//    //    /// </summary>
//    //    public void AddRegistrar(IIocDependencyRegistrar registrar)
//    //    {
//    //        _registrars.Add(registrar);
//    //    }

//    //    /// <summary>
//    //    /// 检测类型是否注册
//    //    /// </summary>
//    //    public bool IsRegistered(Type type)
//    //    {
//    //        return IocContainer.Kernel.HasComponent(type);
//    //    }

//    //    /// <summary>
//    //    /// 检测类型是否注册
//    //    /// </summary>
//    //    public bool IsRegistered<T>()
//    //    {
//    //        return IocContainer.Kernel.HasComponent(typeof(T));
//    //    }

//    //    public void Dispose()
//    //    {
//    //        IocContainer.Dispose();
//    //    }
//    //}
//}
