using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Domain
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum EnumOperateType
    {
        Default = 0,
        自定义 = 1,
        添加 = 2,
        修改 = 3,
        移除 = 4,
        查询 = 5,
        确认 = 6,
        作废 = 7,
        撤消 = 8,
        回退 = 9
    }
}
