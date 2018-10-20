namespace UTH.Module.Meeting
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
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 会议信息业务
    /// </summary>
    public class MeetingApplication :
        ApplicationCrudService<IMeetingDomainService, MeetingEntity, MeetingDTO, MeetingEditInput>,
        IMeetingApplication
    {
        public MeetingApplication(IMeetingDomainService _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }

        #region override

        protected override MeetingEditInput InsertBefore(MeetingEditInput input)
        {
            var maxNum = service.Max<string>(field: x => x.Num);
            if (maxNum.IsEmpty())
            {
                input.Num = "100000";
            }
            else
            {
                input.Num = StringHelper.Get(IntHelper.Get(maxNum) + 1);
            }

            return base.InsertBefore(input);
        }

        #endregion

        /// <summary>
        /// 根据Code获取会议信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MeetingDTO GetByCode(MeetingEditInput input)
        {
            return MapperToDto(service.Find(where: x => x.Num == input.Num).FirstOrDefault());
        }

        /// <summary>
        /// 获取同步数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual MeetingSyncOutput SyncQuery(MeetingSyncInput input)
        {
            return service.FindSyncQuery(input);
        }
    }
}
