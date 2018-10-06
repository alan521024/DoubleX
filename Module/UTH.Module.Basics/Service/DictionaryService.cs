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
    /// 数据字典业务
    /// </summary>
    public class DictionaryService : ApplicationDefault<DictionaryEntity, DictionaryEditInput, DictionaryOutput>, IDictionaryService
    {
        #region 构造函数

        public DictionaryService(IRepository<DictionaryEntity> _repository) : base(_repository)
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

        public override Func<DictionaryEditInput, DictionaryEntity, DictionaryEntity> UpdateBeforeCall => (input, entity) =>
        {
            entity.Name = input.Name;
            return entity;
        };

        public override Expression<Func<DictionaryEntity, bool>> FindPredicate(QueryInput input)
        {
            if (!input.IsNull() && !input.Query.IsNull())
            {
                var exp = ExpressHelper.Get<DictionaryEntity>();

                string key = input.Query.GetString("key"), name = input.Query.GetString("name");
                int DictionaryType = input.Query.GetInt("assetsType");

                exp = exp.AndIF(!key.IsEmpty(), x => (x.Name).Contains(key));
                exp = exp.AndIF(!name.IsEmpty(), x => x.Name.Contains(name));
                //exp = exp.AndIF(!DictionaryType.IsEmpty(), x => x.DictionaryType == DictionaryType);
                return exp.ToExpression();
            }

            return base.FindPredicate(input);
        }

        #endregion
    }
}
