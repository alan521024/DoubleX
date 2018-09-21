namespace UTH.Meeting.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Reflection;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 会议系统应用
    /// </summary>
    public class ApplicationConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Meeting.Domain"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return "MeetingDomain"; } }

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
        public bool IsBusiness { get; set; } = false;

        /// <summary>
        /// 是否插件组件
        /// </summary>
        public bool IsPlug { get; set; } = false;

        /// <summary>
        /// 组件安装
        /// </summary>
        public void Install()
        {

        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Shutdown()
        {

        }
    }
}
