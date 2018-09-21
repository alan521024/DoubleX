namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Xml.Serialization;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 动态Api配置服务
    /// </summary>
    public class DynamicApiConfigObjService : DefaultConfigObjService<DynamicApiConfigModel>, IDynamicApiConfigObjService
    {
        public DynamicApiConfigObjService()
        {
        }

        private static readonly Lazy<DynamicApiConfigModel> lazyConfig = new Lazy<DynamicApiConfigModel>(() =>
        {
            var model = new DynamicApiConfigObjService().Load();
            //InitializationConfig(model);
            return model;
        });

        public static DynamicApiConfigModel Instance
        {
            get
            {
                return lazyConfig.Value;
            }
        }

        public override string FileName
        {
            get
            {
                return "DynamicApi.config";
            }
        }
    }
}