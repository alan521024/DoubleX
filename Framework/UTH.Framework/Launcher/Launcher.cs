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
    /// 应用程序启动器（实始化，启动前期准备（IOC、初始配置）/启动服务Hosting）
    /// </summary>
    public class Launcher
    {
        IHosting appHosting { get; set; }

        public Launcher Startup(IHosting hosting)
        {
            hosting.CheckNull();
            appHosting = hosting;
            appHosting.Startup();
            return this;
        }

        public void OnStart(object obj = null)
        {
            if (appHosting != null && appHosting.OnStart != null)
            {
                appHosting.OnStart(obj);
            }
        }

        public void OnStop(object obj = null)
        {
            if (appHosting != null && appHosting.OnStop != null)
            {
                appHosting.OnStop(obj);
            }
        }

        public void OnBegin(object obj = null)
        {
            if (appHosting != null && appHosting.OnBegin != null)
            {
                appHosting.OnBegin(obj);
            }
        }

        public void OnEnd(object obj = null)
        {
            if (appHosting != null && appHosting.OnEnd != null)
            {
                appHosting.OnEnd(obj);
            }
        }
    }
}
