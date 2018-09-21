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
    //* Record audio using a variety of capture APIs (各种方式捕获音频)
    //* WaveIn
    //* WASAPI
    //* ASIO

    /// <summary>
    /// 录音操作辅助类
    /// </summary>
    public static class RecordingHelper
    {
        /// <summary>
        /// 麦克风列表
        /// </summary>
        /// <returns></returns>
        public static List<KeyValueModel<int, WaveInCapabilities>> Microphones()
        {
            List<KeyValueModel<int, WaveInCapabilities>> list = new List<KeyValueModel<int, WaveInCapabilities>>();

            int devices = WaveIn.DeviceCount;
            for (int i = 0; i < devices; i++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(i);
                if (!list.Any(x => x.Value.ProductName == deviceInfo.ProductName))
                {
                    list.Add(new KeyValueModel<int, WaveInCapabilities>(i, deviceInfo));
                }
            }

            return list;


        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        public static List<MMDevice> MMDevices(out MMDevice defaultDevice)
        {
            var enumerator = new MMDeviceEnumerator();
            defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            return enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
        }
    }
}
