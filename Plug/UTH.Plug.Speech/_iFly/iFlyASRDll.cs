using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 讯飞识别接口方法
    /// </summary>
    public class iFlyASRDll
    {
        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRSessionBegin(string grammarList, string _params, ref int errorCode);//开始一路会话

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRGrammarActivate(string sessionID, string grammar, string type, int weight);//激活语法

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRAudioWrite(string sessionID, IntPtr waveData, uint waveLen, AudioStatus audioStatus, ref EpStatus epStatus, ref RecogStatus recogStatus);//写音频数据

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRGetResult(string sessionID, ref RecogStatus rsltStatus, int waitTime, ref int errorCode);//得到识别结果

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRSessionEnd(string sessionID, string hints);//结束一路会话

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);//读取语法
    }
}
