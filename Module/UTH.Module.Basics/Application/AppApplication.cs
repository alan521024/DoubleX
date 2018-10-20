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
    using UTH.Plug;

    /// <summary>
    /// 应用程序应用服务
    /// </summary>
    public class AppApplication :
        ApplicationCrudService<AppEntity, AppDTO, AppEditInput>,
        IAppApplication
    {
        IDomainDefaultService<AppVersionEntity> versionService;
        public AppApplication(IDomainDefaultService<AppEntity> _service, IDomainDefaultService<AppVersionEntity> _versionService, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
            versionService = _versionService;
        }

        #region override

        protected override AppEditInput InsertBefore(AppEditInput input)
        {
            var isExist = service.Find(where: x => x.Name == input.Name || x.Code == input.Code);
            if (!isExist.IsEmpty())
            {
                throw new DbxException(EnumCode.提示消息, isExist.Where(x => x.Name == input.Name).Count() > 0 ? Lang.sysMingChengYiCunZai : Lang.sysBianMaYiCunZai);
            }
            return input;
        }

        protected override AppEntity UpdateBefore(AppEditInput input, AppEntity entity)
        {
            var isExist = service.Find(where: x => x.Name == input.Name || x.Code == input.Code);
            if (!isExist.IsEmpty())
            {
                if (isExist.Where(x => x.Name == input.Name && x.Id != input.Id).Count() > 0)
                {
                    throw new DbxException(EnumCode.提示消息, Lang.sysMingChengYiCunZai);
                }
                if (isExist.Where(x => x.Code == input.Code && x.Id != input.Id).Count() > 0)
                {
                    throw new DbxException(EnumCode.提示消息, Lang.sysBianMaYiCunZai);
                }
            }

            entity.Name = input.Name;
            entity.AppType = input.AppType;
            entity.Code = input.Code;
            entity.Key = input.Key;
            entity.Secret = input.Secret;
            return entity;
        }

        protected override Expression<Func<AppEntity, bool>> InputToWhere(QueryInput<AppDTO> input)
        {
            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AppEntity>();

                var key = "";//input.Query.GetString("key");
                var appType = EnumsHelper.Get<EnumAppType>(input.Query.AppType);

                exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                exp = exp.AndIF(appType != EnumAppType.Default, x => x.AppType == appType);

                return exp.ToExpression();
            }
            return base.InputToWhere(input);
        }

        #endregion

        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public ApplicationModel GetModel(string appCode)
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

            var model = EngineHelper.Map<ApplicationModel>(app);
            model.Versions = EngineHelper.Map<ApplicationVersion>(currentVersion);
            model.Versions.No = new Version(currentVersion.No);
            return model;
        }
    }
}
