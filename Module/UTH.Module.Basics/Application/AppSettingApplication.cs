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
    /// 会议信息应用服务
    /// </summary>
    public class AppSettingApplication :
        ApplicationCrudService<IAppSettingDomainService, AppSettingEntity, AppSettingDTO, AppSettingEditInput>,
        IAppSettingApplication
    {
        public AppSettingApplication(IAppSettingDomainService _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }

        #region override

        protected override Expression<Func<AppSettingEntity, bool>> InputToWhere(QueryInput<AppSettingDTO> input)
        {

            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AppSettingEntity>();

                exp = exp.AndIF(!input.Query.AppId.IsEmpty(), x => x.AppId == input.Query.AppId);

                return exp.ToExpression();
            }
            return base.InputToWhere(input);
        }

        #endregion


        /// <summary>
        /// 根据AppId获取配置
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public AppSettingDTO GetByApp(Guid appId)
        {
            var entity = service.Get(x => x.AppId == appId);
            if (entity.IsNull())
            {
                entity = new AppSettingEntity();
                entity.UserJson = JsonHelper.Serialize(new UserSetting());
                entity = service.Insert(entity);
            }
            return MapperToDto(entity);
        }
    }
}
