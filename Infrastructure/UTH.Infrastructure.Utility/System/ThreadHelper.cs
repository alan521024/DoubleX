using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 线程操作辅助类
    /// </summary>
    public static class ThreadHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        ///// <summary>
        ///// millisecond
        ///// </summary>


        /// <summary>
        /// 倒计时
        /// </summary>
        /// <param name="action">毫秒</param>
        /// <param name="totalSecond">毫秒</param>
        /// <param name="intervalSecond">毫秒</param>
        public static void Countdown(Action<double> action, double total = 6000, double interval = 1000)
        {
            var current = 0d;
            while (current <= total)
            {
                action(current);
                current += interval;
                Thread.Sleep((int)interval);
            }
        }

        #endregion
    }
}
