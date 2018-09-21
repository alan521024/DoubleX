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
    /// 多媒体操作信息
    /// </summary>
    public static class MultimediaHelper
    {
        /// <summary>
        /// 获取计算语音字节长度
        /// </summary>
        public static uint GetAudioByteLength(int _rate = 16000, int _bitDepth = 16, int _bufferMilliseconds = 150)
        {
            return (uint)(_rate * (_bitDepth / 8) * ((float)_bufferMilliseconds / 1000));
        }
    }
}
