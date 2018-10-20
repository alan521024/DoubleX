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
    /// 会议领域服务接口
    /// </summary>
    public interface IMeetingDomainService : IDomainDefaultService<MeetingEntity>
    {
        /// <summary>
        /// 查找同步记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MeetingSyncOutput FindSyncQuery(MeetingSyncInput input);
    }
}
