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
    /// 应用程序业务
    /// </summary>
    public class AppService : ApplicationDefault<AppEntity, AppEditInput, AppOutput>, IAppService
    {
        #region 构造函数

        public AppService(IRepository<AppEntity> _repository) : base(_repository)
        {
        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作


        #endregion

        #region 重写操作

        public override Action<AppEditInput> InsertBeforeCall => base.InsertBeforeCall;
        public override Func<AppOutput, AppOutput> InsertAfterCall => base.InsertAfterCall;

        public override Func<AppEditInput, AppEntity, AppEntity> UpdateBeforeCall => (input, entity) =>
        {
            entity.Name = input.Name;
            entity.AppType = input.AppType;
            return entity;
        };
        public override Func<AppOutput, AppOutput> UpdateAfterCall => base.UpdateAfterCall;

        public override Expression<Func<AppEntity, bool>> FindPredicate(QueryInput param)
        {
            var exp = ExpressHelper.Get<AppEntity>();

            #region AppInput

            if (!param.IsNull() && !param.Query.IsNull())
            {
                string key = param.Query.GetString("key"), name = param.Query.GetString("name");
                int appType = param.Query.GetInt("appType");

                exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                exp = exp.AndIF(!appType.IsEmpty(), x => x.AppType == appType);
            }

            #endregion

            return exp.ToExpression();
        }

        #endregion

        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public ApplicationModel GetModel(string appCode)
        {
            var app = Find(predicate: x => x.Code == appCode).FirstOrDefault();
            if (app.IsNull())
            {
                return null;
            }

            var appVersionService = EngineHelper.Resolve<IAppVersionService>();
            var appVersions = appVersionService.Find(predicate: x => x.AppId == app.Id);
            if (appVersions.IsEmpty())
            {
                return null;
            }

            var currentVersion = appVersions.OrderByDescending(x => x.No).FirstOrDefault();

            var model = EngineHelper.Map<ApplicationModel>(app);
            model.Versions= EngineHelper.Map<ApplicationVersion>(currentVersion);
            model.Versions.No = new Version(currentVersion.No);
            return model;
        }
    }
}
