namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 应用设置应用服务接口
    /// </summary>
    public interface IAppSettingApplication :
        IApplicationCrudService<AppSettingDTO, AppSettingEditInput>,
        IApplicationService
    {
        /// <summary>
        /// 根据AppId获取配置
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        AppSettingDTO GetByApp(Guid appId);
    }
}
