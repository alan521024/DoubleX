namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Autofac.Extras.DynamicProxy;
    using Castle.DynamicProxy;
    using AutoMapper;
    using AutoMapper.Configuration;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// Autofac
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// 当前配置
        /// </summary>
        private static WebAppServiceOptions current = new WebAppServiceOptions();

        /// <summary>
        /// 使用Autofac IOC 组件(如使用动态Api 必须)
        /// </summary>
        public static AutofacServiceProvider AddAutofacProvider(this IMvcBuilder mvcBuilder, IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, IHosting hosting, Action<WebAppServiceOptions> optionAction = null)
        {
            //set options
            optionAction?.Invoke(current);

            var builder = EngineHelper.GetBuilder<ContainerBuilder>();

            var feature = new ControllerFeature();
            mvcBuilder.PartManager.PopulateFeature(feature);

            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();

            //注意使用class,并不是业务的接口，这里己经将service实现列视为控制器类了
            var register = builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray())
                    .EnableClassInterceptors(new ProxyGenerationOptions()
                    {
                        Hook = new ActionProxyHook(),
                        Selector = new ServiceInterceptorSelector()
                    });
            if (!current.Interceptors.IsEmpty())
            {
                register.InterceptedBy(current.Interceptors);
            };
            register.PropertiesAutowired();


            builder.Populate(services);

            return new AutofacServiceProvider(EngineHelper.ContainerBuilder<IContainer>());
        }
    }
}
