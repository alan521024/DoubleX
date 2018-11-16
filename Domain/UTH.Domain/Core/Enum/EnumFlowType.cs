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

    /// <summary>
    /// 处理流程
    /// </summary>
    public enum EnumFlowType
    {
        /// <summary>
        /// 默认 null
        /// </summary>
        Default,
        /// <summary>
        /// 用户导入
        /// </summary>
        UserImport,
    }
}
