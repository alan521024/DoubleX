namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 组件配置信息接口
    /// </summary>
    public interface IComponentConfiguration
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// 组件名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 显示名称
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        Assembly Assemblies { get; }

        /// <summary>
        /// 是否业务组件
        /// </summary>
        bool IsBusiness { get; set; }

        /// <summary>
        /// 是否插件组件
        /// </summary>
        bool IsPlug { get; set; }

        /// <summary>
        /// 组件安装
        /// </summary>
        void Install();

        /// <summary>
        /// 组件卸载
        /// </summary>
        void Uninstall();
    }
}
