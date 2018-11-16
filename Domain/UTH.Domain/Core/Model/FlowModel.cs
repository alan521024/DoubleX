namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 流程信息
    /// </summary>
    public class FlowModel
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 总数据
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当前数据
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误列表
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsOver { get; set; }

    }
}
