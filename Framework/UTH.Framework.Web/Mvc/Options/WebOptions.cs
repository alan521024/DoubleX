namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;

    /// <summary>
    /// Web相目配置
    /// </summary>
    public class WebAppServiceOptions
    {
        /// <summary>
        /// 是否启用动态Api
        /// </summary>
        public bool IsDynamicApi { get; set; } = false;

        /// <summary>
        /// 是否启用Autofac DI
        /// </summary>
        public bool IsAutofacDIService { get; set; } = true;

        /// <summary>
        /// 拦截器列表
        /// </summary>
        public List<Type> Interceptors { get; set; } = new List<Type>() { typeof(IApplicationLoggingInterceptor), typeof(IInputValidatorInterceptor) };

        /// <summary>
        /// 过滤器列表
        /// </summary>
        public FilterCollection Filters { get; } = new FilterCollection();

        /// <summary>
        /// 约定列表
        /// </summary>
        public IList<IApplicationModelConvention> Conventions { get; set; } = new List<IApplicationModelConvention>();

        /// <summary>
        /// MVC 部份类库列表
        /// </summary>
        public IList<ApplicationPart> MvcApplicationParts = new List<ApplicationPart>();

        /// <summary>
        /// MVC 特性列表
        /// </summary>
        public IList<IApplicationFeatureProvider> MvcFeatures { get; } = new List<IApplicationFeatureProvider>();


    }
}
