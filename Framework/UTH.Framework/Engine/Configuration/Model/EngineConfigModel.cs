namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 引擎配置
    /// </summary>
    [Serializable]
    public class EngineConfigModel
    {
        /// <summary>
        /// App命名空间
        /// </summary>
        public string AppNamespace { get; set; }

        /// <summary>
        /// AppCode
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// App名称
        /// </summary>
        public string AppTitle { get; set; }

        /// <summary>
        /// App程序类型: Console,Service,Winfrom,Wpf,Web,ApiService,Wap
        /// </summary>
        public EnumAppType AppType { get; set; }

        /// <summary>
        /// 调试模式: true,false
        /// </summary>
        public bool IsDebugger { get; set; }

        /// <summary>
        /// 区域信息(多个，默认第一个,以','分割)
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// 托管/宿主 地址: http://+:8100 
        /// </summary>
        public string Hosting { get; set; }

        /// <summary>
        /// bind/module 等类库文件路径
        /// </summary>
        public string BinPath { get; set; }

        /// <summary>
        /// 配置文件路径: /Config
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// 验证码过期时间（S/秒）
        /// </summary>
        public long CaptchaExpire { get; set; }

        /// <summary>
        /// 存储配置
        /// </summary>
        public EngineStoreConfigModel Store { get; set; } = new EngineStoreConfigModel();

        /// <summary>
        /// 文件服务
        /// </summary>
        public EngineFileServerModel FileServer { get; set; } = new EngineFileServerModel();

        /// <summary>
        /// 认证授权
        /// </summary>
        public AuthenticationConfigModel Authentication { get; set; } = new AuthenticationConfigModel();

        /// <summary>
        /// Key-Value 集合
        /// </summary>
        [XmlArray("Settings")]
        [XmlArrayItem("Item")]
        public SettingListConfigModel Settings { get; set; } = new SettingListConfigModel();
    }

    /// <summary>
    /// 引擎存储相关配置
    /// </summary>
    public class EngineStoreConfigModel
    {
        /// <summary>
        /// 数据存储
        /// </summary>
        public ConnectionModel Database { get; set; }

        /// <summary>
        /// 默认缓存
        /// </summary>
        public ConnectionModel Caching { get; set; }
    }

    /// <summary>
    /// 文件服务配置
    /// </summary>
    public class EngineFileServerModel
    {
        /// <summary>
        /// 服务名称(默认本地)
        /// </summary>
        public string Name { get; set; } = "Local";

        /// <summary>
        /// 上传路径
        /// </summary>
        public string Upload { get; set; } = "Upload";

        /// <summary>
        /// 下载路径
        /// </summary>
        public string Download { get; set; }
    }

    /// <summary>
    /// 认证配置
    /// </summary>
    public class AuthenticationConfigModel
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public EnumAuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// issuer 请求实体，可以是发起请求的用户的信息，也可是jwt的签发者。
        /// </summary>
        public string Issuer { get; set; } = "UTH Auth Server";

        /// <summary>
        /// 订阅者
        /// 对订阅进行验证，减少转发攻击。例如，一个接收令牌的站点不能将其重放到另一个。一个转发的令牌将包含原始站点的订阅。
        /// </summary>
        public List<string> Audiences { get; set; } = new List<string>();

        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginPath { get; set; } = "/account/login";

        /// <summary>
        /// 退出地址
        /// </summary>
        public string LogoutPath { get; set; } = "/account/logout";

        /// <summary>
        /// 无权限地址
        /// </summary>
        public string AccessDeniedPath { get; set; } = "/error/access";

        /// <summary>
        /// 密钥Key
        /// JWT Key 必须是16个字符
        /// </summary>
        public string SecretKey { get; set; } = "abcdefghijkhim1234567890";

        /// <summary>
        /// 过期时间(S/秒)
        /// 默认3600(30分)
        /// </summary>
        public long ExpireTime { get; set; } = 3600;

        /// <summary>
        /// Token存储连接
        /// </summary>
        public ConnectionModel TokenStore { get; set; }
    }

    /// <summary>
    ///  Key-Value 集合
    /// </summary>
    public class SettingListConfigModel : List<SettingItemConfigModel>
    {
    }

    /// <summary>
    /// Key-Value配置项
    /// </summary>
    public class SettingItemConfigModel
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }

    /// <summary>
    ///  Key-Value 集合 操作扩展
    /// </summary>
    public static class SettingListConfigModelExtend
    {
        /// <summary>
        /// 获取配置扩展
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(this SettingListConfigModel config, string key)
        {
            if (config.IsNull())
                return string.Empty;
            var item = config.Where(x => x.Key == key).FirstOrDefault();
            return item?.Value;
        }
    }

}
