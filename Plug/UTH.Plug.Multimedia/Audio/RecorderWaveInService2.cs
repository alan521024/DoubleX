using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Plug.Multimedia
{
    /// <summary>
    /// 录音器(WaveIn)
    /// </summary>
    public class RecorderWaveInService2 : IRecorderService<WaveInCapabilities>
    {
        public RecorderWaveInService2(Action<RecorderOption<WaveInCapabilities>> action = null)
        {
            options = new RecorderOption<WaveInCapabilities>();
            Configruation(action);
        }

        #region 公共属性

        #endregion

        #region 私有变量

        /// <summary>
        /// 录音配置
        /// </summary>
        private RecorderOption<WaveInCapabilities> options { get; set; }

        /// <summary>
        /// 音频捕获
        /// </summary>
        private WaveIn waveIn { get; set; }

        /// <summary>
        /// 写录音文件
        /// </summary>
        private WaveFileWriter wavFile { get; set; }

        #endregion

        #region 辅助操作

        /// <summary>
        /// 计算音量
        /// 适用16bit单通/8bit双通
        /// </summary>
        private int GetVolumeValue(object sender, WaveInEventArgs e)
        {
            //ref:https://bbs.csdn.net/topics/392302881?page=1
            try
            {
                //double sumVolume = 0.0;
                //double avgVolume = 0.0;
                //double volume = 0.0;
                //int length = e.Buffer.Length;
                //for (int i = 0; i < length; i += 2)
                //{
                //    int v1 = e.Buffer[i] & 0xFF;
                //    int v2 = e.Buffer[i + 1] & 0xFF;
                //    int temp = v1 + (v2 << 8);// 小端
                //    if (temp >= 0x8000)
                //    {
                //        temp = 0xffff - temp;
                //    }
                //    sumVolume += Math.Abs(temp);
                //}
                //avgVolume = sumVolume / length / 2;
                //volume = Math.Log((1 + avgVolume), 10) * 10;
                //return (int)volume;

                long sh = System.BitConverter.ToInt64(e.Buffer, 0);
                //if (sh > this.maxvol) { this.maxvol = sh; }
                //if (sh < this.minvol) { this.minvol = sh; }
                long width = (long)Math.Pow(2, 50);
                float svolume = Math.Abs(sh / width);
                if (svolume > 1500.0f) { svolume = 1500.0f; }
                if (svolume < 50.0f) { svolume = 50.0f; }
                return (int)(svolume / 15.0f);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void CloseDispose()
        {
        }

        #endregion

        /// <summary>
        /// 配置设置
        /// </summary>
        /// <param name="action"></param>
        public void Configruation(Action<RecorderOption<WaveInCapabilities>> action)
        {
            if (!action.IsNull())
            {
                action.Invoke(options);
            }
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void Start(Action<RecorderOption<WaveInCapabilities>> action = null)
        {
            Configruation(action);
            Stop();

            options.Device.CheckNull();

            waveIn = new WaveIn();
            waveIn.DeviceNumber = options.DeviceNum;
            waveIn.WaveFormat = new WaveFormat(options.Rate, options.BitDepth, options.Channel);
            waveIn.BufferMilliseconds = options.BufferMilliseconds;
            waveIn.NumberOfBuffers = !options.NumberOfBuffers.IsNull() ? options.NumberOfBuffers.Value : waveIn.NumberOfBuffers;

            if (!options.FileName.IsEmpty())
            {
                FilesHelper.CreateFold(options.FileName, isFilePath: true);
                FilesHelper.DeleteFile(options.FileName);
                wavFile = new WaveFileWriter(options.FileName, waveIn.WaveFormat);
            }

            waveIn.DataAvailable += (sender, e) =>
            {
                options.DataEvent?.Invoke(sender, e);
                options.VolumeEvent?.Invoke(GetVolumeValue(sender, e));
                wavFile?.Write(e.Buffer, 0, e.BytesRecorded);
            };

            waveIn.RecordingStopped += (sender, e) =>
            {
                options.StopedEvent?.Invoke(sender, e);
            };

            waveIn.StartRecording();
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void Stop()
        {
            waveIn?.StopRecording();
            waveIn?.Dispose();
            waveIn = null;

            wavFile?.Dispose();
            wavFile = null;
        }
    }
}
