using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UTH.Plug.Speech
{
    /// <summary>
    /// 讯飞DLL 方法
    /// </summary>
    public class iFlyBaseDll
    {
        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSPLogin(string usr, string pwd, string strparams);

        [DllImport(@"Library\msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSPLogout();//释放
    }
}
