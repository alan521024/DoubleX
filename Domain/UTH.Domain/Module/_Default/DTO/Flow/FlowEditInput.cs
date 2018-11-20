namespace UTH.Domain
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
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 流程信息输入
    /// </summary>
    public class FlowEditInput : IInput
    {
        /// <summary>
        /// 流程类型
        /// </summary>
        public EnumFlowType FlowType { get; set; }

        /// <summary>
        /// 有效时间(默认 1000 * 60 * 30 30分钟)
        /// </summary>
        public int Expire { get; set; } = 1800000;

        /// <summary>
        /// 参数信息
        /// </summary>
        public dynamic Param { get; set; }
    }
}
