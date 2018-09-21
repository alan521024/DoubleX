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
    /// 运行引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 引擎配置信息
        /// </summary>
        EngineConfigModel Config { get; }

        /// <summary>
        /// IOC管理器
        /// </summary>
        IIocManager IocManager { get; }
    }
}
