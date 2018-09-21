using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Plug.Multimedia
{
    /// <summary>
    /// 录音配置
    /// </summary>
    public class RecorderOption<TDevice>
    {
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
        /// 使用的缓冲区数量（通常为2或3）
        /// </summary>
        public int? NumberOfBuffers { get; set; }
        
        /// <summary>
        /// 设备号
        /// </summary>
        public int DeviceNum { get; set; } = 0;

        /// <summary>
        /// 录音设备
        /// </summary>
        public TDevice Device { get; set; }

        /// <summary>
        /// 录音文件
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 录音数据到达事件
        /// Byte[] 接收到数据
        /// </summary>
        public Action<object, WaveInEventArgs> DataEvent { get; set; }

        /// <summary>
        /// 录音停止事件
        /// string 停止消息,如存在意思失败
        /// </summary>
        public Action<object,StoppedEventArgs> StopedEvent { get; set; }

        /// <summary>
        /// 音亮事件(1-100)
        /// </summary>
        public Action<float> VolumeEvent { get; set; }

    }
}
