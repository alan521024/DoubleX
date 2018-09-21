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
    /// 会议记录业务接口
    /// </summary>
    public interface IMeetingRecordService : IApplicationDefault<MeetingRecordEntity, MeetingRecordEditInput, MeetingRecordOutput>
    {
        /// <summary>
        /// 添加会议记录(含翻译记录)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MeetingRecordOutput Add(MeetingRecordEditInput input);
    }
}
