namespace UTH.Plug.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Xml.Serialization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    //***********************
    //
    // (根节点不使用节点Attribute)
    // 区别：<Name></Name> / <item Name="" />
    //
    //***********************

    /// <summary>
    /// 短信配置信息
    /// </summary>
    [Serializable]
    [XmlRoot("Sms")]
    public class SmsConfigModel
    {
        /// <summary>
        /// 默认使用(为Empty视为不使用)
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 短信服务列表(集合方式 扩展配置)
        /// </summary>
        [XmlElement("Platform")]
        public virtual List<SmsPlatformModel> Platforms { get; set; }
    }

    /// <summary>
    /// 短信平台
    /// </summary>           
    public class SmsPlatformModel
    {
        /// <summary>
        /// 平台名称[EnumSmsPlatform](唯一)
        /// </summary>
        [XmlAttribute("Name")]
        public virtual EnumSmsPlatform Name { get; set; }

        /// <summary>
        /// 平台Id
        /// </summary>
        [XmlAttribute("Id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// 平台Key
        /// </summary>
        [XmlAttribute("Key")]
        public virtual string Key { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [XmlAttribute("Secret")]
        public virtual string Secret { get; set; }

        /// <summary>
        /// 注册标识eg: 【UTH】
        /// </summary>
        [XmlAttribute("Ident")]
        public virtual string Ident { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [XmlAttribute("Account")]
        public virtual string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [XmlAttribute("Password")]
        public virtual string Password { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [XmlAttribute("Url")]
        public virtual string Url { get; set; }

    }
}
