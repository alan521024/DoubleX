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
            EngineHelper.RegisterType<IAppRepository, AppRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IAppDomainService, AppDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IAppApplication, AppApplication>(DomainHelper.ApplicationIoc);

            EngineHelper.RegisterType<IAppVersionRepository, AppVersionRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IAppVersionDomainService, AppVersionDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IAppVersionApplication, AppVersionApplication>(DomainHelper.ApplicationIoc);

            EngineHelper.RegisterType<IAppSettingRepository, AppSettingRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IAppSettingDomainService, AppSettingDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IAppSettingApplication, AppSettingApplication>(DomainHelper.ApplicationIoc);

            //dictionary
            EngineHelper.RegisterType<IDictionaryApplication, DictionaryApplication>(DomainHelper.ApplicationIoc);

            //assets
            EngineHelper.RegisterType<IAssetsApplication, AssetsApplication>(DomainHelper.ApplicationIoc);

            //permission
            EngineHelper.RegisterType<INavigationApplication, NavigationApplication>(DomainHelper.ApplicationIoc);
            EngineHelper.RegisterType<IOperateApplication, OperateApplication>(DomainHelper.ApplicationIoc);

        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Uninstall()
        {

        }
    }
}
