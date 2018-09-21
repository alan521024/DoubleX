namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Xml.Serialization;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using System.Reflection;

    //***********************
    //
    // (根节点不使用节点Attribute)
    // 区别：<Name></Name> / <item Name="" />
    //
    //***********************

    /// <summary>
    /// 动态Api配置
    /// </summary>
    [Serializable]
    [XmlRoot("DynamicApi")]
    public class DynamicApiConfigModel : DynamicApiConfigBaseAttribute
    {
        /// <summary>
        /// 服务后缀(控制器名称中将移除)
        /// </summary>
        public virtual string ServicePostfix { get; set; }

        /// <summary>
        /// 组件列表
        /// </summary>
        [XmlElement("Component")]
        public virtual List<DynamicApiComponent> Components { get; set; }
    }

    /// <summary>
    /// 组件配置
    /// </summary> 
    public class DynamicApiComponent : DynamicApiConfigBaseAttribute
    {
        public DynamicApiComponent() { }
        public DynamicApiComponent(DynamicApiComponent setting, DynamicApiConfigModel parent) : base(setting, parent)
        {

        }

        /// <summary>
        /// 模块类型
        /// </summary>
        public Assembly Assemblies { get; set; }

        /// <summary>
        /// 服务类集合
        /// </summary>
        [XmlElement("Controller")]
        public virtual List<DynamicApiControls> Controllers { get; set; }
    }

    /// <summary>
    /// 控制器配置
    /// </summary>
    public class DynamicApiControls : DynamicApiConfigBaseAttribute
    {
        public DynamicApiControls() { }
        public DynamicApiControls(DynamicApiControls setting, DynamicApiComponent parent) : base(setting, parent)
        {

        }

        /// <summary>
        /// 方法集合
        /// </summary>
        [XmlElement("Action")]
        public virtual List<DynamicApiAction> Actions { get; set; }
    }

    /// <summary>
    /// 方法配置
    /// </summary>
    public class DynamicApiAction : DynamicApiConfigBaseAttribute
    {
        public DynamicApiAction() { }
        public DynamicApiAction(DynamicApiAction seeting, DynamicApiControls parent) : base(seeting, parent)
        {

        }

        /// <summary>
        /// 请求方式(Default 根据史称)
        /// </summary>
        [XmlAttribute("Verb")]
        public virtual EnumHttpVerb Verb { get; set; } = EnumHttpVerb.DEFAULT;
    }

    /// <summary>
    /// 基本配置 
    /// </summary>
    [Serializable]
    public class DynamicApiConfigBaseAttribute
    {
        public DynamicApiConfigBaseAttribute() { }
        public DynamicApiConfigBaseAttribute(DynamicApiConfigBaseAttribute setting, DynamicApiConfigBaseAttribute parent)
        {
            if (parent != null)
            {
                Enable = parent.Enable ?? parent.Enable;
                Authorize = parent.Authorize ?? parent.Authorize;
                Route = parent.Route ?? parent.Route;
                ParamBinding = parent.ParamBinding ?? parent.ParamBinding;
                ResultWrapper = parent.ResultWrapper ?? parent.ResultWrapper;
                ExceptionWrapper = parent.ExceptionWrapper ?? parent.ExceptionWrapper;
            }

            if (setting != null)
            {
                Name = setting.Name ?? setting.Name;
                TypeName = setting.TypeName ?? setting.TypeName;

                Enable = string.IsNullOrWhiteSpace(setting.Enable) ? Enable : setting.Enable;
                Authorize = string.IsNullOrWhiteSpace(setting.Authorize) ? Authorize : setting.Authorize;
                Route = string.IsNullOrWhiteSpace(setting.Route) ? Route : setting.Route;
                ParamBinding = string.IsNullOrWhiteSpace(setting.ParamBinding) ? ParamBinding : setting.ParamBinding;
                ResultWrapper = string.IsNullOrWhiteSpace(setting.ResultWrapper) ? ResultWrapper : setting.ResultWrapper;
                ExceptionWrapper = string.IsNullOrWhiteSpace(setting.ExceptionWrapper) ? ExceptionWrapper : setting.ExceptionWrapper;
            }

        }

        /// <summary>
        /// 名称标识
        /// </summary>
        [XmlAttribute("Name")]
        public virtual string Name { get; set; }

        [XmlAttribute("TypeName")]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 开启/关闭
        /// </summary>}
        [XmlAttribute("Enable")]
        public virtual string Enable { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        [XmlAttribute("Authorize")]
        public virtual string Authorize { get; set; }

        /// <summary>
        /// 路由格式
        /// </summary>
        [XmlAttribute("Route")]
        public virtual string Route { get; set; }

        /// <summary>
        /// 叁数绑定(FromBody,...暂全部为FromBody)
        /// </summary>
        [XmlAttribute("ParamBinding")]
        public virtual string ParamBinding { get; set; }

        /// <summary>
        /// 是否返回结果包裹过滤器
        /// </summary>
        [XmlAttribute("ResultWrapper")]
        public virtual string ResultWrapper { get; set; }

        /// <summary>
        /// 是否异常结果包裹过滤器
        /// </summary>
        [XmlAttribute("ExceptionWrapper")]
        public virtual string ExceptionWrapper { get; set; }



    }
}
