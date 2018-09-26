﻿namespace UTH.Framework
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
    /// 应用服务基类实现
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        #region 公共属性

        /// <summary>
        /// 访问会话信息
        /// </summary>
        public IApplicationSession Session { get; set; }
        
        /// <summary>
        /// 应用缓存
        /// </summary>
        public IAppCachingService AppCaching { get; set; }

        /// <summary>
        /// 数据缓存
        /// </summary>
        public IDataCachingService DataCaching { get; set; }

        /// <summary>
        /// 会话缓存
        /// </summary>
        public ISessionCachingService SessionCaching { get; set; }

        #endregion

        #region 业务操作

        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="session"></param>
        public virtual void SetSession(IApplicationSession session)
        {
            Session = session;
        }

        #endregion
    }
}