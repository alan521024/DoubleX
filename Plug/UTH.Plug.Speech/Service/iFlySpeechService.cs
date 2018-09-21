using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Plug.Speech
{
    [Serializable]
    public class iFlySpeechService : ISpeechService
    {
        public iFlySpeechService() { }

        public iFlySpeechService(SpeechOption opt)
        {
            options = opt;
        }

        #region 公共属性

        #endregion

        #region 私有变量

        /// <summary>
        /// 配置信息
        /// </summary>
        SpeechOption options = new SpeechOption();

        /// <summary>
        /// 语别任务
        /// </summary>
        Thread speechTask;

        /// <summary>
        /// 数据对列
        /// </summary>
        volatile ConcurrentQueue<SpeechData> _queue = new ConcurrentQueue<SpeechData>();

        string param, session, sentence;
        AudioStatus audio;
        DateTime preResultDt = DateTime.MinValue;

        #endregion

        /// <summary>
        /// 配置设置
        /// </summary>
        /// <param name="action"></param>
        public void Configruation(Action<SpeechOption> action)
        {
            action?.Invoke(options);
        }

        /// <summary>
        /// 开始操作
        /// </summary>
        public void Start()
        {
            ClearWork();

            param = string.Format("sub=iat,language={0},domain=iat,accent={1},sample_rate={2},asr_denoise=1,aue=speex-web,result_type=plain,vad_enable=0,vad_eos={3},result_encoding={4}",
                options.Language, options.Accent, options.Rate, options.Speed, options.Encode);

            audio = AudioStatus.ISR_AUDIO_SAMPLE_FIRST;

            int ret = iFlyBaseDll.MSPLogin(null, null, "appid=" + options.AppId);

            speechTask = new Thread(SpeechWork);
            speechTask.IsBackground = true;
            speechTask.Start();

            options.OnStart?.Invoke(ret);
        }

        /// <summary>
        /// 停止操作
        /// </summary>
        public void Stop()
        {
            ClearWork();
            int ret = iFlyBaseDll.MSPLogout();
            options.OnStop?.Invoke(ret);
        }

        /// <summary>
        /// 语音数据
        /// </summary>
        public void Send(SpeechData data)
        {
            if (_queue != null && data != null)
            {
                _queue.Enqueue(data);
            }
        }


        /// <summary>
        /// 识别处理
        /// </summary>
        private void SpeechWork()
        {
            while (true)
            {
                if (_queue.Count > 0)
                {
                    SpeechData current = null;
                    _queue.TryDequeue(out current);
                    if (current == null)
                        continue;

                    //区分会议Id  //current.Key
                    WriteData(current.Key, current.Data, current.Offset, current.Length);
                }
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 清除任务
        /// </summary>
        private void ClearWork()
        {
            if (speechTask != null)
            {
                try
                {
                    speechTask.Abort();
                    speechTask = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 写入语音数据进行识别
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int WriteData(Guid key, byte[] data, int offset = 0, int length = 0)
        {
            int ret = 0;

            if (data == null || length == 0)
            {
                return ret;
            }

            if (session.IsEmpty())
            {
                session = Ptr2Str(iFlyASRDll.QISRSessionBegin(null, param, ref ret));
            }

            if (ret != 0)
            {
                options.OnError?.Invoke(key, string.Format("session / {0}", ret));
                return ret;
            }

            EpStatus point = EpStatus.ISR_EP_NULL;
            RecogStatus recog = RecogStatus.ISR_REC_NULL;

            ret = iFlyASRDll.QISRAudioWrite(session, ByteToIntPtr(data), options.ByteLength, audio, ref point, ref recog);
            if (ret != 0)
            {
                options.OnError?.Invoke(key, string.Format("write / {0}", ret));
                return ret;
            }
            audio = AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE;

            string result = "";
            if (recog == RecogStatus.ISR_REC_STATUS_SUCCESS)
            {
                result = Ptr2Str(iFlyASRDll.QISRGetResult(session, ref recog, 0, ref ret));
            }
            if (ret != 0)
            {
                options.OnError?.Invoke(key, string.Format("result / {0}", ret));
                return ret;
            }

            //识别结果
            if (!result.IsEmpty())
            {
                sentence += result;
                preResultDt = DateTime.Now;

                options.OnResult?.Invoke(key, result, recog == RecogStatus.ISR_REC_STATUS_SPEECH_COMPLETE);
            }

            //内容断句
            if (!sentence.IsEmpty() && (recog == RecogStatus.ISR_REC_STATUS_SPEECH_COMPLETE || DateTime.Now > preResultDt.AddMilliseconds(300)))
            {
                options.OnSentence?.Invoke(key, sentence);
                sentence = "";
                preResultDt = DateTime.Now;
            }

            //当前识别结束，重新开始，且重新开始会话(只有识别成功不管是否为整句：point == EpStatus.ISR_EP_AFTER_SPEECH)
            if (recog == RecogStatus.ISR_REC_STATUS_SPEECH_COMPLETE || point == EpStatus.ISR_EP_AFTER_SPEECH)
            {
                if (!session.IsEmpty())
                {
                    iFlyASRDll.QISRSessionEnd(session, string.Empty);
                }
                session = null;
                options.OnMessage?.Invoke(key, string.Format("Audio[{0}] Endpoint[{1}]  Recog[{2}] Ret[{3}]", audio, point, recog, ret));
            }

            return ret;
        }


        /// <summary>
        /// 指针转字符串
        /// </summary>
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        private string Ptr2Str(IntPtr p)
        {
            try
            {
                List<byte> lb = new List<byte>();
                while (Marshal.ReadByte(p) != 0)
                {
                    lb.Add(Marshal.ReadByte(p));
                    p = p + 1;
                }
                byte[] bs = lb.ToArray();
                return Encoding.Default.GetString(lb.ToArray());
            }
            catch (Exception ex)
            {
                //Logger.LogError("指针转字符串错误（Ptr2Str）:", ex);
                return null;
            }
        }

        /// <summary>
        /// byte数据转指针转字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IntPtr ByteToIntPtr(byte[] data)
        {

            IntPtr bp = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, bp, data.Length);//将缓存考入IntPtr结构数据
            return bp;
        }
    }
}
