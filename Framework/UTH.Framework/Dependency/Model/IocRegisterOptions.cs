﻿namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Ioc注入 注册配置
    /// </summary>
    public class IocRegisterOptions
    {
        /// <summary>
        /// 按名称注册
        /// 仅支持方法：
        ///     RegisterType[T, TService](IocRegisterOptions option)
        ///     RegisterType(Type type, Type service, IocRegisterOptions option)
        /// </summary>
        public string Named { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<KeyValueModel<string, object>> Parameters { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<KeyValueModel<string, object>> Properties { get; set; }

        /// <summary>
        /// Aop代理配置(ProxyGenerationOptions)
        /// </summary>
        public dynamic InterceptorProxy { get; set; }

        /// <summary>
        /// Aop拦截器
        /// </summary>
        public Type[] InterceptorTypes { get; set; }

        /// <summary>
        /// 是否单例
        /// </summary>
        public bool SingleInstance { get; set; } = false;

    }
}