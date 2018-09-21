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
    /// 录音机接口
    /// </summary>
    public interface IRecorderService<T>
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        void Configruation(Action<RecorderOption<T>> action);

        /// <summary>
        /// 开始录音
        /// </summary>
        void Start(Action<RecorderOption<T>> action = null);

        /// <summary>
        /// 结束录音
        /// </summary>
        void Stop();
    }
}
