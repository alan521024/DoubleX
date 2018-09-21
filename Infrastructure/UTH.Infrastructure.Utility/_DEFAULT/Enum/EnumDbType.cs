using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum EnumDbType
    {
        Default = 0,
        MySql = 1,
        SqlServer = 2,
        Sqlite = 3,
        Oracle = 4,
        Redis = 5,
        Mongodb = 6,
        MemoryCache = 7,
    }
}
