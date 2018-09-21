namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac.Core;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// IOC注入 解析方法
    /// </summary>
    public interface IIocResolver
    {
        T Resolve<T>();

        T Resolve<T>(Type type);

        T Resolve<T>(IEnumerable<KeyValueModel<string, object>> parameters);

        T Resolve<T>(string name, IEnumerable<KeyValueModel<string, object>> parameters = null);

        object Resolve(Type type);

        object Resolve(Type type, object parameters);

        IEnumerable<T> ResolveAll<T>();

        IEnumerable<T> ResolveAll<T>(object parameters);

        IEnumerable<object> ResolveAll(Type type);
       
        IEnumerable<object> ResolveAll(Type type, object parameters);

    }
}
