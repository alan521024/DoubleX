namespace UTH.Module
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Reflection;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 基础组件配置
    /// </summary>
    public class ModuleConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Module"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return ""; } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get { return "基础组件"; } }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assemblies { get { return this.GetType().Assembly; } }

        /// <summary>
        /// 是否业务组件
        /// </summary>
        public bool IsBusiness { get; set; } = true;

        /// <summary>
        /// 是否插件组件
        /// </summary>
        public bool IsPlug { get; set; } = false;

        /// <summary>
        /// 组件安装
        /// </summary>
        public void Install()
        {
            EngineHelper.RegisterType<ISecurityCodeService, SecurityCodeService>();
            EngineHelper.RegisterType<INotifyService, NotifyService>();

            EngineHelper.RegisterType<ICaptchaApplication, CaptchaApplication>();
            EngineHelper.RegisterType<INotifyApplication, NotifyApplication>();

            EngineHelper.RegisterType<IFlowService, FlowService>();
            EngineHelper.RegisterType<IFlowApplication, FlowApplication>();

            EngineHelper.RegisterType<INotifyInterceptor, NotifyInterceptor>();
            EngineHelper.RegisterType<ICaptchaInterceptor, CaptchaInterceptor>();
        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Uninstall()
        {

        }
    }
}
