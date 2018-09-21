namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 引擎配置服务
    /// </summary>
    public class EngineConfigObjService : DefaultConfigObjService<EngineConfigModel>, IEngineConfigObjService
    {
        public EngineConfigObjService()
        {
        }

        const string ConfigEngineCacheFormat = "_CONFIG_ENGINE_{0}";

        private static readonly Lazy<EngineConfigModel> lazyConfig = new Lazy<EngineConfigModel>(() =>
        {
            return new EngineConfigObjService().Load();
        });

        public static EngineConfigModel Instance
        {
            get
            {
                return lazyConfig.Value;
            }
        }

        public override string CacheKey
        {
            get
            {
                return string.Format(ConfigEngineCacheFormat, AppDomain.CurrentDomain.FriendlyName);
            }
        }

        public override string FileName
        {
            get
            {
                return "Engine.config";
            }
        }

        public override string FilePath
        {
            get
            {
                if (EngineHelper.GlobalPath.IsEmpty())
                {
                    EngineHelper.GlobalPath = FilesHelper.GetPath(FileName, isAppWork: true);
                    if (!File.Exists(EngineHelper.GlobalPath))
                    {
                        throw new Exception(string.Format(Lang.sysConfigEngineError, EngineHelper.GlobalPath));
                    }
                }
                return EngineHelper.GlobalPath;
            }
        }
    }
}
