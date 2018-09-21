namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using System.ComponentModel;

    /// <summary>
    /// MVC模型初始化
    /// </summary>
    public class DynamicApiProvider : IApplicationModelProvider
    {
        public int Order { get { return -1000 + 10; } }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            // Intentionally empty.
        }

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            // Intentionally empty.
        }
    }
}
