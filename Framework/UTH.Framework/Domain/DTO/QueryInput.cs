using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UTH.Infrastructure.Utility;

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
    /// 默认查询参数信息
    /// </summary>
    public class QueryInput<TQueryInput>
    {
        public QueryInput()
        {

        }

        public QueryInput(QueryInput param)
        {
            if (!param.IsNull())
            {
                this.Page = param.Page;
                this.Size = param.Size;
                this.Query = default(TQueryInput);
                this.Sorting = param.Sorting;
                if (!param.Query.IsNull())
                {
                    this.Query = JsonHelper.Deserialize<TQueryInput>(param.Query);
                }
            }
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
    
        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 查询对象
        /// </summary>
        public TQueryInput Query { get; set; }

        /// <summary>
        /// 排序对象
        /// </summary>
        public List<KeyValueModel> Sorting { get; set; }
    }

    /// <summary>
    /// 默认查询参数信息
    /// </summary>
    public class QueryInput : QueryInput<JObject>
    {

    }
}
