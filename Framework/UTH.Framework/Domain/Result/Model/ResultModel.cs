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
    /// 返回结果对象
    /// </summary>
    public class ResultModel<TEntity> : IResultModel
    {
        public ResultModel() : this(EnumCode.成功, default(TEntity))
        {
        }

        public ResultModel(TEntity obj) : this(EnumCode.成功, obj)
        {
        }

        public ResultModel(EnumCode code, TEntity obj)
        {
            Code = code;
            Obj = obj;
        }

        public EnumCode Code { get; set; }

        public string Message { get; set; }

        public TEntity Obj { get; set; }

    }

    /// <summary>
    /// 返回结果对象
    /// </summary>
    public class ResultModel : ResultModel<object>,IResultModel
    {
        public ResultModel() : base(EnumCode.成功, default(object))
        {
        }

        public ResultModel(EnumCode code) : base(code, default(object))
        {
        }

        public ResultModel(object obj) : base(EnumCode.成功, obj)
        {
        }

        public ResultModel(EnumCode code, object obj) : base(EnumCode.成功, obj)
        {
        }
    }
}
