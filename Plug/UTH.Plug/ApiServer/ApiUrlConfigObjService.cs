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
    /// Api地址配置服务
    /// </summary>
    public class ApiUrlConfigObjService : DefaultConfigObjService<ApiUrlConfigModel>, IApiUrlConfigObjService
    {
        public ApiUrlConfigObjService()
        {
        }

        private static readonly Lazy<ApiUrlConfigModel> lazyConfig = new Lazy<ApiUrlConfigModel>(() =>
        {
            return new ApiUrlConfigObjService().Load();
        });

        public static ApiUrlConfigModel Instance
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
                return "ApiUrl.config";
            }
        }
    }
}