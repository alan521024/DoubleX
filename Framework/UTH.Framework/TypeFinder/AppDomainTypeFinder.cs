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
    using System.IO;
    using System.Text.RegularExpressions;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 应用程序类型查找器
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        #region 构造函数

        public AppDomainTypeFinder()
        {
            LoadAssemblies();
        }

        #endregion

        #region 私有变量

        private bool ignoreReflectionErrors = true;

        private string assemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        private string assemblyRestrictToLoadingPattern = ".*";

        /// <summary>
        /// 已加载的程序集
        /// </summary>
        private List<Assembly> _assemblies = new List<Assembly>();

        /// <summary>
        /// 是否已加载
        /// </summary>
        private bool isLoaded = false;

        /// <summary>
        /// 用应程序
        /// </summary>
        public AppDomain app = AppDomain.CurrentDomain;

        #endregion

        #region 公共属性

        /// <summary>
        /// 程序集合
        /// </summary>
        public List<Assembly> Assemblies
        {
            get
            {
                if (!isLoaded)
                {
                    LoadAssemblies();
                }
                return _assemblies;
            }
        }

        #endregion

        #region 辅助操作

        /// <summary>
        /// 加载应用程序集
        /// </summary>
        private void LoadAssemblies()
        {
            if (isLoaded)
                return;

            isLoaded = true;

            //app domain 
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Matches(assembly.FullName))
                {
                    if (!_assemblies.Contains(assembly))
                    {
                        _assemblies.Add(assembly);
                    }
                }
            }

            //paths
            GetConfigPaths().ForEach(p =>
            {
                if (!Directory.Exists(p))
                    return;

                foreach (string dllPath in Directory.GetFiles(p, "*.dll", SearchOption.AllDirectories))
                {
                    try
                    {
                        //var an = AssemblyName.GetAssemblyName(dllPath);
                        //_assemblies.Add(app.Load(an));

                        Assembly dll = Assembly.LoadFile(dllPath);
                        if (!dll.IsNull() && Matches(dll.FullName) && !_assemblies.Select(x => x.GetName().FullName).Contains(dll.FullName))
                        {
                            _assemblies.Add(dll);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(string.Format("  过滤： {0}加载", dllPath));
                        Console.WriteLine(string.Format("  原因： {0}", !string.IsNullOrWhiteSpace(ex.Message) ? ex.Message : ex.ToString()));
                        Console.WriteLine("");
                    }
                }
            });

        }

        /// <summary>
        /// 获取程序集目录
        /// </summary>
        /// <returns></returns>
        private List<string> GetConfigPaths()
        {
            List<string> paths = new List<string>();

            //not hosted. For example, run either in unit tests
            //string path = HostingEnvironment.EnvironmentName;
            //if (HostingEnvironment.IsHosted)
            //{
            //    return HttpRuntime.BinDirectory;
            //}

            paths.Add(AppDomain.CurrentDomain.BaseDirectory);

            if (!EngineHelper.Configuration.BinPath.IsEmpty())
            {
                EngineHelper.Configuration.BinPath.Split('|').ToList().ForEach(x =>
                {
                    if (x.Contains(":"))
                    {
                        paths.Add(x);
                    }
                    else
                    {
                        FilesHelper.GetPath(x, isAppWork: true);
                    }
                });
            }

            return paths;
        }

        /// <summary>
        /// 检查一个dll是否是我们知道不需要调查的已发送的dll之一。
        /// </summary>
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, assemblySkipLoadingPattern)
                   && Matches(assemblyFullName, assemblyRestrictToLoadingPattern);
        }

        /// <summary>
        /// 检查一个dll是否是我们知道不需要调查的已发送的dll之一。
        /// </summary>
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 判断类型实现是否泛型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGeneric"></param>
        /// <returns></returns>
        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 重写操作

        #endregion

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, Assemblies, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch (Exception ex)
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!ignoreReflectionErrors)
                        {
                            throw;
                        }
                    }
                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (assignTypeFrom.IsAssignableFrom(t) ||
                                (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            {
                                if (!t.IsInterface)
                                {
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.IsClass && !t.IsAbstract)
                                        {
                                            result.Add(t);
                                        }
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                                }
                            }
                            else
                            {
                                if (assignTypeFrom.IsGenericType && assignTypeFrom.IsGenericAssignableFrom(t, typeIsOnlyGenericType: false))
                                {
                                    if (!t.IsInterface)
                                    {
                                        if (onlyConcreteClasses)
                                        {
                                            if (t.IsClass && !t.IsAbstract)
                                            {
                                                result.Add(t);
                                            }
                                        }
                                        else
                                        {
                                            result.Add(t);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                Debug.WriteLine(fail.Message, fail);

                throw fail;
            }
            return result;
        }

    }
}
