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
    /// 应用宿主接口
    /// </summary>
    public interface IHosting
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        Guid Key { get; }

        /// <summary>
        /// 应用开启事件
        /// </summary>
        Action<Object> OnStart { get; }

        /// <summary>
        /// 应用停止事件
        /// </summary>
        Action<Object> OnStop { get; }

        /// <summary>
        /// 操作开始事件
        /// </summary>
        Action<Object> OnBegin { get; }

        /// <summary>
        /// 操作结束事件
        /// </summary>
        Action<Object> OnEnd { get; }

        /// <summary>
        /// Hosting启动
        /// </summary>
        void Startup();
    }
}
