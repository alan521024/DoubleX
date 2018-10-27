using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Plug.Speech
{
    public interface ISpeechService
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        void Configruation(Action<SpeechOption> action);

        /// <summary>
        /// 开始操作
        /// </summary>
        void Start();

        /// <summary>
        /// 卸载操作
        /// </summary>
        void Stop();

        /// <summary>
        /// 发送数据
        /// </summary>
        void Send(SpeechData data);

        /// <summary>
        /// 重新开始
        /// </summary>
        void Restart();
    }
}
