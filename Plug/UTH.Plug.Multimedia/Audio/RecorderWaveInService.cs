//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using NAudio.Wave;
//using NAudio.CoreAudioApi;
//using UTH.Infrastructure.Resource.Culture;
//using UTH.Infrastructure.Utility;
//using UTH.Framework;

//namespace UTH.Plug.Multimedia
//{
//    /// <summary>
//    /// 录音器(WaveIn)
//    /// </summary>
//    public class RecorderWaveInService : IRecorderService<WaveInCapabilities>
//    {
//        public RecorderWaveInService()
//        {
//        }

//        public RecorderWaveInService(RecorderOption<WaveInCapabilities> option)
//        {
//            if (option != null)
//            {
//                Option = option;
//            }
//        }

//        public RecorderWaveInService(Action<RecorderOption<WaveInCapabilities>> action)
//        {
//            Configruation(action);
//        }

//        #region 公共属性

//        /// <summary>
//        /// 是否停止
//        /// </summary>
//        public bool Stoped { get; set; } = true;

//        /// <summary>
//        /// 音亮大小
//        /// </summary>
//        public float Volume { get; set; } = 1;

//        #endregion

//        #region 私有变量

//        /// <summary>
//        /// 录音配置
//        /// </summary>
//        private RecorderOption<WaveInCapabilities> Option { get; set; } = new RecorderOption<WaveInCapabilities>();

//        /// <summary>
//        /// 音频捕获
//        /// </summary>
//        private WaveIn waveIn { get; set; }
//        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;

//        #endregion

//        #region 辅助操作

//        /// <summary>
//        /// 设置设备配置
//        /// </summary>
//        private void SetOption()
//        {
//            waveIn.DeviceNumber = Option.DeviceNum;
//            waveIn.WaveFormat = new WaveFormat(Option.Rate, Option.BitDepth, Option.Channel);
//            waveIn.BufferMilliseconds = Option.BufferMilliseconds;
//            if (!Option.NumberOfBuffers.IsNull())
//            {
//                waveIn.NumberOfBuffers = Option.NumberOfBuffers.Value;
//            }

//            waveIn.DataAvailable += (sender, e) =>
//            {
//                Option.DataEvent?.Invoke(sender, e);
//                synchronizationContext.Post(s =>
//                {
//                    Option.VolumeEvent?.Invoke(GetVolumeValue(sender, e));
//                }, null);
//                Stoped = false;
//            };

//            waveIn.RecordingStopped += (sender, e) =>
//            {
//                waveIn?.Dispose();
//                waveIn = null;
//                Option.StopedEvent?.Invoke(sender, e);
//                Stoped = true;
//            };
//        }

//        /// <summary>
//        /// 计算音量
//        /// 适用16bit单通/8bit双通
//        /// </summary>
//        private int GetVolumeValue(object sender, WaveInEventArgs e)
//        {
//            //ref:https://bbs.csdn.net/topics/392302881?page=1
//            try
//            {
//                //double sumVolume = 0.0;
//                //double avgVolume = 0.0;
//                //double volume = 0.0;
//                //int length = e.Buffer.Length;
//                //for (int i = 0; i < length; i += 2)
//                //{
//                //    int v1 = e.Buffer[i] & 0xFF;
//                //    int v2 = e.Buffer[i + 1] & 0xFF;
//                //    int temp = v1 + (v2 << 8);// 小端
//                //    if (temp >= 0x8000)
//                //    {
//                //        temp = 0xffff - temp;
//                //    }
//                //    sumVolume += Math.Abs(temp);
//                //}
//                //avgVolume = sumVolume / length / 2;
//                //volume = Math.Log((1 + avgVolume), 10) * 10;
//                //return (int)volume;

//                long sh = System.BitConverter.ToInt64(e.Buffer, 0);
//                //if (sh > this.maxvol) { this.maxvol = sh; }
//                //if (sh < this.minvol) { this.minvol = sh; }
//                long width = (long)Math.Pow(2, 50);
//                float svolume = Math.Abs(sh / width);
//                if (svolume > 1500.0f) { svolume = 1500.0f; }
//                if (svolume < 50.0f) { svolume = 50.0f; }
//                return (int)(svolume / 15.0f);
//            }
//            catch (Exception ex)
//            {
//                return 0;
//            }
//        }

//        #endregion

//        /// <summary>
//        /// 配置设置
//        /// </summary>
//        /// <param name="action"></param>
//        public void Configruation(Action<RecorderOption<WaveInCapabilities>> action)
//        {
//            action?.Invoke(Option);
//        }

//        /// <summary>
//        /// 开始录音
//        /// </summary>
//        public void Start()
//        {
//            Option.CheckNull();
//            Option.Device.CheckNull();

//            waveIn = new WaveIn();
//            SetOption();
//            waveIn.StartRecording();

//            Stoped = false;
//        }

//        /// <summary>
//        /// 停止录音
//        /// </summary>
//        public void Stop()
//        {
//            Stoped = true;
//            waveIn?.StopRecording();
//        }
//    }
//}
