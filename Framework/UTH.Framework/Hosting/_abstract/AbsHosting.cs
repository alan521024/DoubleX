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
    /// 应用宿主默认实现(抽象，必须子类详细)
    /// </summary>
    public abstract class AbsHosting : IHosting
    {
        
        //写法：直接实例后不能调用实现接口方法
        //IUTHHost.xxxxx

        /// <summary>
        /// 主键标识
        /// </summary>
        public abstract Guid Key { get;}

        /// <summary>
        /// 应用开启事件
        /// </summary>
        public abstract Action<Object> OnStart { get; }

        /// <summary>
        /// 应用停止事件
        /// </summary>
        public abstract Action<Object> OnStop { get; }

        /// <summary>
        /// 操作开始事件
        /// </summary>
        public abstract Action<Object> OnBegin { get;}

        /// <summary>
        /// 操作结束事件
        /// </summary>
        public abstract Action<Object> OnEnd { get;}

        /// <summary>
        /// Hosting启动
        /// </summary>
        public abstract void Startup();

    }
}
