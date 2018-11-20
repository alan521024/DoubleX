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
    /// 会议配置领域服务接口
    /// </summary>
    public interface IMeetingProfileDomainService : IDomainDefaultService<MeetingProfileEntity>
    {
        /// <summary>
        /// 获取账号会议配置(不存在时创建默认)
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        MeetingProfileEntity GetOrInsertDefaultByAccount(Guid accountId);
    }
}
