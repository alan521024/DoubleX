namespace UTH.Plug
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
    using UTH.Framework;

    /// <summary>
    /// Api地址配置服务接口
    /// </summary>
    public interface IApiUrlConfigObjService : IConfigObjService<ApiUrlConfigModel>
    {
    }
}
