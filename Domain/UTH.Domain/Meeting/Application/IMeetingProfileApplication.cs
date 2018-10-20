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
    /// 会议账号配置应用服务接口
    /// </summary>
    public interface IMeetingProfileApplication : 
        IApplicationCrudService<MeetingProfileDTO, MeetingProfileEditInput>,
        IApplicationService
    {
        /// <summary>
        /// 获取当前登录账号(Session)配置,如不存在创建
        /// </summary>
        /// <returns></returns>
        MeetingProfileDTO GetLoginAccountProfile();

        /// <summary>
        /// 保存当前登录账号(Session)配置
        /// </summary>
        /// <returns></returns>
        MeetingProfileDTO SaveLoginAccountProfile(MeetingProfileEditInput input);
    }
}
