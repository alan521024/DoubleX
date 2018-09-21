namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 会议配置信息信息(DTO/JSON)
    /// </summary>
    [Serializable]
    public class MeetingSettingModel
    {
        /// <summary>
        /// 语速(高：10 中:5  低:1)
        /// </summary>
        public int Speed { get; set; } = 5;

        /// <summary>
        /// 采样率，固定值 8k/16k  default:16000
        /// </summary>
        public int Rate { get; set; } = 16000;

        /// <summary>
        /// 声道数 default: 1
        /// </summary>
        public int Channel { get; set; } = 1;

        /// <summary>
        ///  位深
        ///  8k/16k 采样率 16bit 位深的单声道
        /// </summary>
        public int BitDepth { get; set; } = 16;

        /// <summary>
        /// 毫秒的缓冲区。推荐值为100 ms
        /// </summary>
        public int BufferMilliseconds { get; set; } = 100;

        /// <summary>
        /// 停止数据后时间断句。推荐值为1500 ms
        /// </summary>
        public int SentenceMilliseconds { get; set; } = 1500;

        /// <summary>
        /// 内容长度(托管对象)
        /// </summary>
        public uint ByteLength { get; set; }


        /// <summary>
        /// 会议模块个人配置Id
        /// </summary>
        public Guid ProfileId { get; set; }

        /// <summary>
        /// 所属账号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 源语言
        /// </summary>
        public string SourceLang { get; set; }

        /// <summary>
        /// 目标语言
        /// </summary>
        public string TargetLangs { get; set; }

        /// <summary>
        /// 口音/方言
        /// </summary>
        public string Accent { get; set; } = "mandarin";
        
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Encode { get; set; } = "UTF-8";

        /// <summary>
        /// 远程服务地址
        /// eg:"ipc://channel/ServerRemoteObject.rem"
        /// </summary>
        public string RemoteAddress { get; set; }
    }
}
