using System;
using System.Collections.Generic;
using System.Text;


namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 应用程序类型
    /// </summary>
    [Flags]
    public enum EnumAppType
    {
        Console = 1,
        Service = 2,
        Winfrom = 3,
        Wpf = 4,
        Web = 5,
        Api = 6,
        Wap = 7,
    }
}
