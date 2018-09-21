namespace UTH.Plug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.IO;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    public class OpenApiBaseModel<T>
    {
        // <summary>
        /// 返回结果代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        public T Obj { get; set; }
    }
}
