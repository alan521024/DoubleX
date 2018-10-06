namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 应用程序输出信息
    /// </summary>
    public class AppOutput : AppBase, IOutput
    {
        /// <summary>
        /// 应用类型名称
        /// </summary>
        public string AppTypeName { get { return AppType.GetName(); } }
    }
}
