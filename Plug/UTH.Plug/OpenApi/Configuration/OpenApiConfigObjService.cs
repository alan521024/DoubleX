namespace UTH.Plug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.IO;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 开放接口配置服务
    /// </summary>
    public class OpenApiConfigObjService : DefaultConfigObjService<OpenApiConfigModel>, IOpenApiConfigObjService
    {
        public OpenApiConfigObjService()
        {
        }

        private static readonly Lazy<OpenApiConfigModel> lazyConfig = new Lazy<OpenApiConfigModel>(() =>
        {
            return new OpenApiConfigObjService().Load();
        });

        public static OpenApiConfigModel Instance
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
                return "OpenApi.config";
            }
        }
    }
}