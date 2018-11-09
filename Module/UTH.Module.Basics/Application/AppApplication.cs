namespace UTH.Module.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 应用程序应用服务
    /// </summary>
    public class AppApplication :
        ApplicationCrudService<IAppDomainService, AppEntity, AppDTO, AppEditInput, AppQuery>,
        IAppApplication
    {
        IDomainDefaultService<AppVersionEntity> versionService;
        public AppApplication(IAppDomainService _service, IDomainDefaultService<AppVersionEntity> _versionService, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
            versionService = _versionService;
        }

        #region override

        protected override Expression<Func<AppEntity, bool>> InputToWhere(QueryInput<AppQuery> input)
        {
            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AppEntity>();

                exp = exp.AndIF(!input.Query.Search.IsEmpty(), x => (x.Name + x.Code).Contains(input.Query.Search));

                return exp.ToExpression();
            }
            return base.InputToWhere(input);
        }

        #endregion

        /// <summary>
        /// 应用程序详细信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AppDetail Detail(string code)
        {
            var detail = new AppDetail();
            detail.Application = EngineHelper.Map<AppDTO>(service.Find(where: x => x.Code == code).FirstOrDefault());
            detail.Versions = EngineHelper.Map<AppVersionDTO>(versionService.Find(where: x => x.AppId == detail.Application.Id).OrderBy(x=>x.No).Last());
            detail.DownloadUrl = $"{EngineHelper.Configuration.FileServer.DownUrl}/api/assets/download";
            return detail;
        }

        #region 过时方法

        /// <summary>
        /// 获取应用信息(旧版)
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [Obsolete("This function is obsolete")]
        public AppOld GetModel(string appCode)
        {
            var app = service.Find(where: x => x.Code == appCode).FirstOrDefault();
            if (app.IsNull())
            {
                return null;
            }

            var appVersions = versionService.Find(where: x => x.AppId == app.Id);
            if (appVersions.IsEmpty())
            {
                return null;
            }

            var currentVersion = appVersions.OrderByDescending(x => x.No).FirstOrDefault();

            var model = EngineHelper.Map<AppOld>(app);
            model.Versions = EngineHelper.Map<AppVersionOld>(currentVersion);
            model.Versions.No = new Version(currentVersion.No);
            return model;
        }

        #endregion
    }
}
