namespace UTH.Plug
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

    /// <summary>
    /// 开放接口配置
    /// </summary>
    [Serializable]
    [XmlRoot("OpenApi")]
    public class OpenApiConfigModel
    {
        /// <summary>
        /// OpenApi接口Url
        /// </summary>
        public virtual string ApiUrl { get; set; } = "http://";

        /// <summary>
        /// OpenApi Key
        /// </summary>
        public virtual string ApiKey { get; set; }


        /// <summary>
        /// 文本翻译(Get)
        /// {{baseUrl}}/api/tmxtranslate?key={{key}}source=zs target=en Quality= q=你好
        /// </summary>
        public virtual string TranslationGet { get; set; } = "/api/tmxtranslate";


        /// <summary>
        /// Speech 语音识别
        /// </summary>
        public virtual string Speech { get; set; } = "/api/voice/text";

    }
}
