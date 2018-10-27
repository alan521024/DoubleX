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
    public class AppVersionApplication :
        ApplicationCrudService<IAppVersionDomainService, AppVersionEntity, AppVersionDTO, AppVersionEditInput>,
        IAppVersionApplication
    {
        public AppVersionApplication(IAppVersionDomainService _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }

        #region override

        protected override Expression<Func<AppVersionEntity, bool>> InputToWhere(QueryInput<AppVersionDTO> input)
        {

            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AppVersionEntity>();

                exp = exp.AndIF(!input.Query.AppId.IsEmpty(), x => x.AppId == input.Query.AppId);

                return exp.ToExpression();
            }
            return base.InputToWhere(input);
        }

        #endregion
    }
}
