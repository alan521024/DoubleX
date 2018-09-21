using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 进程操作辅助类
    /// </summary>
    public static class ProcessHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        /// <summary>
        /// 判断进程是否存在
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public static bool IsExist(string procName)
        {
            return Process.GetProcessesByName(procName).Length > 0;
        }

        /// <summary>
        /// 判断进程是否存在
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public static bool IsExist(int proId)
        {
            return !Process.GetProcessById(proId).IsNull();
        }

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="working"></param>
        /// <param name="args">会对字符串以空格进行分割成数组</param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Process Start(string filePath, string working = null, string args = null, ProcessWindowStyle style = ProcessWindowStyle.Hidden)
        {
            try
            {
                ProcessStartInfo Info = new ProcessStartInfo()
                {
                    FileName = filePath,
                    WindowStyle = style
                };

                if (!string.IsNullOrWhiteSpace(working))
                {
                    Info.WorkingDirectory = working;
                }

                if (!string.IsNullOrWhiteSpace(args))
                {
                    Info.Arguments = args;
                }

                return Process.Start(Info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 进程杀死
        /// </summary>
        /// <param name="procName"></param>
        public static void Kill(string procName)
        {
            //精确进程名  用GetProcessesByName
            var procs = Process.GetProcessesByName(procName);
            var ids = new int[procs.Length];
            for (var i = 0; i < procs.Length; i++)
            {
                ids[i] = procs[i].Id;
                //不使用procs直接杀出，根据名称（多线程或先杀后立马启用新进程/根据名称杀没操作完，就启用新的，可能把新的也同时杀死）;
                //Trace.WriteLine($"id: {ids[i]}");
            }
            Kill(ids);
        }

        /// <summary>
        /// 进程杀死
        /// </summary>
        /// <param name="ids"></param>
        public static void Kill(params int[] ids)
        {
            //https://www.cnblogs.com/tonge/p/4434322.html
            if (ids != null && ids.Length > 0)
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    Process process = null;
                    try
                    {
                        process = Process.GetProcessById(ids[i]);
                    }
                    catch { }
                    process?.Kill();
                }
            }
        }

        #endregion
    }
}
