using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UTH.Framework
{
    /// <summary>
    /// UTH 认证授权服务
    /// </summary>
    public interface IAuthorizeService : ITransientDependency
    {
        Task AuthorizeAsync(string permissions);

        Task AuthorizeAsync(MethodInfo methodInfo, Type type);
    }
}
