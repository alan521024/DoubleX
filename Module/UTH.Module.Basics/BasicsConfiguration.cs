namespace UTH.Module.Basics
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
    using UTH.Plug;

    /// <summary>
    /// 基础组件配置
    /// </summary>
    public class BasicsConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Module.Basics"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return "Basics"; } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get { return "基础设施"; } }

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
            //app
            EngineHelper.RegisterType<IAppRepository, AppRepository>(DomainConfiguration.Options.IocRepositoryOption);

            EngineHelper.RegisterType<IAppApplication, AppApplication>(DomainConfiguration.Options.IocServiceOption);
            EngineHelper.RegisterType<IAppVersionApplication, AppVersionApplication>(DomainConfiguration.Options.IocServiceOption);
            
            //dictionary
            EngineHelper.RegisterType<IDictionaryApplication, DictionaryApplication>(DomainConfiguration.Options.IocServiceOption);

            //assets
            EngineHelper.RegisterType<IAssetsApplication, AssetsApplication>(DomainConfiguration.Options.IocServiceOption);

            //permission
            EngineHelper.RegisterType<INavigationApplication, NavigationApplication>(DomainConfiguration.Options.IocRepositoryOption);
            EngineHelper.RegisterType<IOperateApplication, OperateApplication>(DomainConfiguration.Options.IocRepositoryOption);

        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Uninstall()
        {

        }
    }
}
