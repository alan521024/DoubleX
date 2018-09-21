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
    /// 动态Api配置服务接口
    /// </summary>
    public interface IDynamicApiConfigObjService : IConfigObjService<DynamicApiConfigModel>
    {
    }
}
