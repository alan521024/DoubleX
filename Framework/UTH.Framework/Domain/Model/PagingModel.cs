namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 分页结果
    /// </summary>
    public class PagingModel<TOutput>
    {
        public List<TOutput> Rows { get; set; }
        public long Total { get; set; }
    }
}
