using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 识别配置
    /// </summary>
    [Serializable]
    public class SpeechOption
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

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
        /// 内容长度(托管对象)
        /// </summary>
        public uint ByteLength { get; set; }

        /// <summary>
        /// 毫秒的缓冲区。推荐值为100 ms
        /// </summary>
        public int BufferMilliseconds { get; set; } = 100;

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; } = "zh_cn";

        /// <summary>
        /// 口音/方言
        /// </summary>
        public string Accent { get; set; } = "mandarin";

        /// <summary>
        /// 编辑
        /// </summary>
        public string Encode { get; set; } = "UTF-8";

        /// <summary>
        /// 语速(780 一般)
        /// </summary>
        public int Speed { get; set; } = 780;

        /// <summary>
        /// 开始事件
        /// </summary>
        public Action<object> OnStart { get; set; }

        /// <summary>
        /// 结果事件
        /// </summary>
        public Action<object> OnStop { get; set; }

        /// <summary>
        /// 结果回调
        /// </summary>
        public Action<Guid, string, bool> OnResult { get; set; }

        /// <summary>
        /// 段/句回调
        /// </summary>
        public Action<Guid, string> OnSentence { get; set; }


        /// <summary>
        /// 消息回调
        /// </summary>
        public Action<Guid, string> OnMessage { get; set; }

        /// <summary>
        /// 错误回调
        /// </summary>
        public Action<Guid, string> OnError { get; set; }
    }
}
