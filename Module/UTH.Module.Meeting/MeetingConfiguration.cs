namespace UTH.Module.Meeting
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
    /// 会议组件配置
    /// </summary>
    public class MeetingConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Module.Meeting"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return "Meet"; } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get { return "会议系统"; } }

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
            //meeting
            EngineHelper.RegisterType<IMeetingRepository, MeetingRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IMeetingDomainService, MeetingDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IMeetingApplication, MeetingApplication>(DomainHelper.ApplicationIoc);

            //record
            EngineHelper.RegisterType<IMeetingRecordDomainService, MeetingRecordDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IMeetingRecordApplication, MeetingRecordApplication>(DomainHelper.ApplicationIoc);

            //translation
            EngineHelper.RegisterType<IMeetingTranslationApplication, MeetingTranslationApplication>(DomainHelper.ApplicationIoc);

            //Profile
            EngineHelper.RegisterType<IMeetingProfileDomainService, MeetingProfileDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IMeetingProfileApplication, MeetingProfileApplication>(DomainHelper.ApplicationIoc);
        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Uninstall()
        {

        }

        /// <summary>
        /// 组件配置
        /// </summary>
        private void Configuration()
        {
        }
    }
}
