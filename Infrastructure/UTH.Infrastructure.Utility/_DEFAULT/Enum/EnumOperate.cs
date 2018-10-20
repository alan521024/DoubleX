namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum EnumOperate
    {
        Default,
        增加,
        删除,
        修改,
        查询,
        打印,
        发送系统通知,
        发送邮箱,
        发送短信,
        发送语音,
        发送站内信,
        图片显示,
        校验
    }
}
