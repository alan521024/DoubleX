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
    /// 默认引擎
    /// </summary>
    public class EngineDefault : IEngine
    {
        /// <summary>
        /// 引擎配置信息
        /// </summary>
        public EngineConfigModel Config
        {
            get { return EngineConfigObjService.Instance; }
        }

        /// <summary>
        /// 依赖管理器
        /// </summary>
        public IIocManager IocManager
        {
            get { return AutofacIocManager.Instance; }
        }
    }
}
