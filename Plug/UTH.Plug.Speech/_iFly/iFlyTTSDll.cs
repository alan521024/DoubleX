using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 讯飞语音合成接口
    /// </summary>
    public class iFlyTTSDll
    {
        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTTSSessionBegin(string _params, ref int errorCode);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QTTSTextPut(string sessionID, string textString, uint textLen, string _params);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTTSAudioGet(string sessionID, ref int audioLen, ref SynthStatus synthStatus, ref int errorCode);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTTSAudioInfo(string sessionID);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QTTSSessionEnd(string sessionID, string hints);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QTTSGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);
    }
}
