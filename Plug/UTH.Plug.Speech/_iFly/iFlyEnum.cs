using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 用来告知MSC音频发送是否完成
    /// </summary>
    public enum AudioStatus
    {
        /// <summary>
        /// 设备初始
        /// </summary>
        ISR_AUDIO_SAMPLE_INIT = 0,
        /// <summary>
        /// 第一块音频
        /// </summary>
        ISR_AUDIO_SAMPLE_FIRST = 1,
        /// <summary>
        /// 还有后继音频
        /// </summary>
        ISR_AUDIO_SAMPLE_CONTINUE = 2,
        /// <summary>
        /// 最后一块音频
        /// </summary>
        ISR_AUDIO_SAMPLE_LAST = 4

        //MSP_AUDIO_SAMPLE_INIT = 0x00,
        //MSP_AUDIO_SAMPLE_FIRST = 0x01,
        //MSP_AUDIO_SAMPLE_CONTINUE = 0x02,
        //MSP_AUDIO_SAMPLE_LAST = 0x04,

    }

    /// <summary>
    /// 端点检测（End-point detected）器所处的状态
    /// </summary>
    public enum EpStatus
    {
        /// <summary>
        /// -1
        /// </summary>
        [Description("-1")]
        ISR_EP_NULL = -1,
        /// <summary>
        /// 还没有检测到音频的前端点
        /// </summary>
        ISR_EP_LOOKING_FOR_SPEECH = 0,
        /// <summary>
        /// 已经检测到了音频前端点，正在进行正常的音频处理
        /// </summary>
        ISR_EP_IN_SPEECH = 1,
        /// <summary>
        /// 检测到音频的后端点，后继的音频会被MSC忽略
        /// </summary>
        ISR_EP_AFTER_SPEECH = 3,
        /// <summary>
        /// 超时
        /// </summary>
        ISR_EP_TIMEOUT = 4,
        /// <summary>
        /// 出现错误
        /// </summary>
        ISR_EP_ERROR = 5,
        /// <summary>
        /// 音频过大
        /// </summary>
        ISR_EP_MAX_SPEECH = 6
    }

    /// <summary>
    /// 识别器返回的状态，提醒用户及时开始\停止获取识别结果
    /// </summary>
    public enum RecogStatus
    {
        /// <summary>
        /// -1
        /// </summary>
        ISR_REC_NULL = -1,
        /// <summary>
        /// 识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。
        /// </summary>
        ISR_REC_STATUS_SUCCESS = 0,
        /// <summary>
        /// 识别结束，没有识别结果
        /// </summary>
        ISR_REC_STATUS_NO_MATCH = 1,
        /// <summary>
        /// 正在识别中
        /// </summary>
        ISR_REC_STATUS_INCOMPLETE = 2,
        /// <summary>
        /// 发现有效音频
        /// </summary>
        ISR_REC_STATUS_SPEECH_DETECTED = 4,
        /// <summary>
        /// 识别结束
        /// </summary>
        ISR_REC_STATUS_SPEECH_COMPLETE = 5,
        /// <summary>
        /// 没有发现音频
        /// </summary>
        ISR_REC_STATUS_NO_SPEECH_FOUND = 10
    }

    public enum SynthStatus
    {
        TTS_FLAG_STILL_HAVE_DATA = 1,
        TTS_FLAG_DATA_END,
        TTS_FLAG_CMD_CANCELED
    }
}
