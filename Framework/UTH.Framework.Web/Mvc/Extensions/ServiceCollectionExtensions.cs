namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 当前配置
        /// </summary>
        private static WebAppServiceOptions current = new WebAppServiceOptions();

        /// <summary>
        /// 添加 Web 服务
        /// </summary>
        public static IMvcBuilder AddWeb(this IServiceCollection services, IConfiguration configuration, IHosting hosting, Action<WebAppServiceOptions> optionAction = null)
        {
            //set options
            optionAction?.Invoke(current);

            //see https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //use di to create controllers
            if (current.IsAutofacDIService)
            {
                services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            }

            //application model provider
            if (current.IsDynamicApi)
            {
                services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, DynamicApiProvider>());
            }

            //add transient
            services.AddTransient<IAccessor, WebAccessor>();
            services.AddTransient<IApplicationSession, IdentifierSession>();
            services.AddTransient<ITokenService, TokenService>();

            //routing
            services.AddRouting(routingOpt =>
            {
                routingOpt.LowercaseUrls = true;
            });

            //json
            services.Configure<MvcJsonOptions>(json => JsonHelper.Configure(json.SerializerSettings));

            //authentication
            services.AddAuthent();

            //mvc builder
            var builder = services.AddMvc((option) =>
            {
                //conventions
                foreach (var item in current.Conventions)
                {
                    option.Conventions.Add(item);
                }

                if (current.IsDynamicApi)
                {
                    if (!current.MvcFeatures.Any(x => x is DynamicApiConvention))
                    {
                        option.Conventions.Add(new DynamicApiConvention(services));
                    }
                }

                //filters
                foreach (var item in current.Filters)
                {
                    option.Filters.Add(item);
                }

                //model binders
                //..........

            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            //mvc application parts
            foreach (var item in current.MvcApplicationParts)
            {
                builder.PartManager.ApplicationParts.Add(item);
            }

            //mvc feature
            foreach (var item in current.MvcFeatures)
            {
                builder.PartManager.FeatureProviders.Add(item);
            }

            if (current.IsDynamicApi)
            {
                if (!current.MvcFeatures.Any(x => x is DynamicApiFeature))
                {
                    builder.PartManager.FeatureProviders.Add(new DynamicApiFeature());
                }
            }

            return builder;
        }
    }
}
