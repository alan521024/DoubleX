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
//    /// 录音器(WASAPI)
//    /// </summary>
//    public class RecorderWASAPIService :IRecorderService<MMDevice>
//    {
//        public RecorderWASAPIService()
//        {
//        }
//        public RecorderWASAPIService(Action<RecorderOption<MMDevice>> action)
//        {
//            Configruation(action);
//        }

//        /// <summary>
//        /// 音频捕获
//        /// </summary>
//        private WasapiCapture capture { get; set; }

//        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;

//        /// <summary>
//        /// 录音配置
//        /// </summary>
//        private RecorderOption<MMDevice> Option { get; set; } = new RecorderOption<MMDevice>();

//        /// <summary>
//        /// 是否停止
//        /// </summary>
//        public bool Stoped { get; set; } = true;

//        /// <summary>
//        /// 音亮大小
//        /// </summary>
//        public float Volume { get; set; } = 1;

//        /// <summary>
//        /// 配置设置
//        /// </summary>
//        /// <param name="action"></param>
//        public void Configruation(Action<RecorderOption<MMDevice>> action)
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

//            //音这级别区间(0-1之间数)
//            Option.Device.AudioEndpointVolume.MasterVolumeLevelScalar = 1;

//            //实例化
//            capture = new WasapiCapture(Option.Device);
//            capture.ShareMode = AudioClientShareMode.Shared;// AudioClientShareMode.Exclusive;
//            capture.WaveFormat = new WaveFormat(Option.Rate, Option.BitDepth, Option.Channel);

//            //开始录音
//            capture.StartRecording();

//            //录音事件
//            capture.DataAvailable += (sender, e) =>
//            {
//                Option.DataEvent?.Invoke(sender, e);
//                synchronizationContext.Post(s =>
//                {
//                    Option.VolumeEvent?.Invoke(Option.Device.AudioMeterInformation.MasterPeakValue * 100);
//                }, null);
//            };

//            capture.RecordingStopped += (sender, e) =>
//            {
//                capture?.Dispose();
//                capture = null;
//                Option.StopedEvent?.Invoke(sender, e);
//            };

//            Stoped = false;
//        }

//        /// <summary>
//        /// 停止录音
//        /// </summary>
//        public void Stop()
//        {
//            Stoped = true;
//            capture?.StopRecording();
//        }
//    }
//}
