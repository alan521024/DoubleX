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
    /// 资源信息业务
    /// </summary>
    public class AssetsService : ApplicationDefault<AssetsEntity, AssetsEditInput, AssetsOutput>, IAssetsService
    {
        #region 构造函数

        public AssetsService(IRepository<AssetsEntity> _repository) : base(_repository)
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

        public override Func<AssetsEditInput, AssetsEntity, AssetsEntity> UpdateBeforeCall => (input, entity) =>
        {
            entity.Name = input.Name;
            entity.AssetsType = input.AssetsType;
            return entity;
        };

        public override Expression<Func<AssetsEntity, bool>> FindPredicate(QueryInput input)
        {
            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<AssetsEntity>();

                string key = input.Query.GetString("key"), name = input.Query.GetString("name");
                int AssetsType = input.Query.GetInt("assetsType");

                exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                //exp = exp.AndIF(!AssetsType.IsEmpty(), x => x.AssetsType == AssetsType);
                return exp.ToExpression();
            }

            return base.FindPredicate(input);
        }

        #endregion
    }
}
