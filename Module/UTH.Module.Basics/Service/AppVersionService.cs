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
    /// 会议信息业务
    /// </summary>
    public class AppVersionService : ApplicationDefault<AppVersionEntity, AppVersionEditInput, AppVersionOutput>, IAppVersionService
    {
        #region 构造函数

        public AppVersionService(IRepository<AppVersionEntity> _repository) : base(_repository)
        {
        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion

        #region 辅助操作


        #endregion

        #region 回调事件

        public override Func<AppVersionEditInput, AppVersionEntity, AppVersionEntity> UpdateBeforeCall => (input, entity) =>
        {
            //entity.Name = input.Name;
            //entity.AppType = input.AppType;
            return entity;
        };

        public override Expression<Func<AppVersionEntity, bool>> FindPredicate(QueryInput input)
        {

            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AppVersionEntity>();

                string key = input.Query.GetString("key"), name = input.Query.GetString("name");
                int appType = input.Query.GetInt("appType");

                //exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                //exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                //exp = exp.AndIF(!appType.IsEmpty(), x => x.AppType == appType);

                return exp.ToExpression();
            }

            return base.FindPredicate(input);
        }

        #endregion
    }
}
