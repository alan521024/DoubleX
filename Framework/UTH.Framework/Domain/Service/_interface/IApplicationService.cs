namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 应用服务接口
    /// </summary>
    public interface IApplicationService : IDependency
    {
        #region 公共属性

        /// <summary>
        /// 访问会话信息
        /// </summary>
        IApplicationSession Session { get; set; }

        /// <summary>
        /// 应用缓存服务
        /// </summary>
        IAppCachingService AppCaching { get; set; }

        /// <summary>
        /// 数据缓存服务
        /// </summary>
        IDataCachingService DataCaching { get; set; }

        /// <summary>
        /// 会话缓存服务
        /// </summary>
        ISessionCachingService SessionCaching { get; set; }

        #endregion

        #region 业务操作

        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="session"></param>
        void SetSession(IApplicationSession session);

        #endregion
    }
}
