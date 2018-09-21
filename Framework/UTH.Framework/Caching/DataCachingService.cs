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
    /// 数据缓存服务
    /// </summary>
    public class DataCachingService : RedisCachingService, IDataCachingService
    {
        public DataCachingService(ConnectionModel model):base(model)
        {
        }
    }
}
