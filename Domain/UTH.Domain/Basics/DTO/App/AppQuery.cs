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
    /// 应用程序查询信息
    /// </summary>
    public class AppQuery : AppDTO, IKeys
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Search { get; set; }
    }
}
