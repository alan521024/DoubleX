namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 树结构业务操作(仓储, 增，修改，删，获取，查询，分页)
    /// </summary>
    public class ApplicationTree<TRep, TEntity, TId, TInput, TOutput> : ApplicationDefault<TRep, TEntity, TId, TInput, TOutput>, IApplicationTree<TRep, TEntity, TId, TInput, TOutput>
        where TEntity : class, IEntityTree<TId>
        where TRep : IRepository<TEntity, TId>
    {
        public ApplicationTree(TRep _repository) : base(_repository)
        {
        }

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        public override TOutput Insert(TInput input)
        {
            setInsert(input);
            return base.Insert(input);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        public override List<TOutput> InsertBatch(List<TInput> inputs)
        {
            setInserts(inputs);
            return base.InsertBatch(inputs);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        public override Task<TOutput> InsertAsync(TInput input)
        {
            setInsert(input);
            return base.InsertAsync(input);
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        public override Task<List<TOutput>> InsertBatchAsync(List<TInput> inputs)
        {
            setInserts(inputs);
            return base.InsertBatchAsync(inputs);
        }

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        public override TOutput Update(TInput input)
        {
            setUpdate(input);
            return base.Update(input);
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        public override List<TOutput> UpdateBatch(List<TInput> inputs)
        {
            setUpdates(inputs);
            return base.UpdateBatch(inputs);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        public override Task<TOutput> UpdateAsync(TInput input)
        {
            setUpdate(input);
            return base.UpdateAsync(input);
        }

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        public override Task<List<TOutput>> UpdateBatchAsync(List<TInput> inputs)
        {
            setUpdates(inputs);
            return base.UpdateBatchAsync(inputs);
        }

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除
        /// </summary>
        public override int Delete(TInput input)
        {
            return repository.Delete(getDeletePredicate(input));
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override int DeleteId(TId id)
        {
            return repository.Delete(getDeletePredicate(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override int DeleteIds(List<TId> ids)
        {
            var exps = getDeletePredicate(ids);
            int count = 0;
            exps.ForEach(s =>
            {
                count += repository.Delete(s);
            });
            return count;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 删除
        /// </summary>
        public override Task<int> DeleteAsync(TInput input)
        {
            return repository.DeleteAsync(getDeletePredicate(input));
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override Task<int> DeleteIdAsync(TId id)
        {
            return repository.DeleteAsync(getDeletePredicate(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override Task<int> DeleteIdsAsync(List<TId> ids)
        {
            var exps = getDeletePredicate(ids);
            int count = 0;
            exps.ForEach(s =>
            {
                count += repository.Delete(s);
            });
            return Task.FromResult(count);
        }

        #endregion

        #endregion

        #region 辅助操作

        private void setInsert(TInput input)
        {
            if (input.IsNull())
                throw new DbxException(EnumCode.参数异常);

            var data = EngineHelper.Map<TEntity>(input);
            if (data.IsNull())
                throw new DbxException(EnumCode.参数异常);

            var model = data as IEntityTree<TId>;
            if (model.IsNull())
                throw new DbxException(EnumCode.参数异常);

            if (model.IsNull())
                return;

            if (model.Id.IsEmpty())
                model.Id = ObjectHelper.GetTypeValue<TId>();

            var parent = Get(model.Parent) as IEntityTree<TId>;
            if (parent.IsNull())
            {
                model.Paths = string.Format("{0}|", model.Id);
                model.Depth = 1;
            }
            else
            {
                model.Paths = string.Format("{0}{1}|", parent.Paths, model.Id);
                model.Depth = parent.Depth + 1;
            }
        }

        private void setInserts(List<TInput> inputs)
        {
            if (inputs.IsEmpty())
                throw new DbxException(EnumCode.参数异常);

            var datas = EngineHelper.Map<List<TEntity>>(inputs);
            if (datas.IsEmpty())
                throw new DbxException(EnumCode.参数异常);

            datas.ForEach((x) =>
            {
                if (x.Id.IsEmpty())
                {
                    x.Id = ObjectHelper.GetTypeValue<TId>();
                }
            });

            var parentIds = datas.Select(x => x.Parent).ToList();
            var parents = repository.Find(0, x => parentIds.Contains(x.Id));
            if (parents.IsEmpty())
                return;

            datas.ForEach(input =>
            {
                var parent = parents.Where(p => StringHelper.IsEqual(StringHelper.Get(input.Parent), StringHelper.Get(p.Id))).FirstOrDefault();
                if (parent.IsNull())
                {
                    input.Paths = string.Format("{0}|", input.Id);
                    input.Depth = 1;
                }
                else
                {
                    input.Paths = string.Format("{0}{1}|", parent.Paths, input.Id);
                    input.Depth = parent.Depth + 1;
                }
            });
        }


        private void setUpdate(TInput input)
        {
            if (input.IsNull() || (!input.IsNull() && input.IsEmpty()))
                throw new DbxException(EnumCode.参数异常);

            var data = EngineHelper.Map<TEntity>(input);
            if (data.IsNull())
                throw new DbxException(EnumCode.参数异常);

            var model = data as IEntityTree<TId>;
            if (model.IsNull())
                throw new DbxException(EnumCode.参数异常);


            var parent = Get(model.Parent) as IEntityTree<TId>;
            if (parent.IsNull())
            {
                model.Paths = string.Format("{0}|", model.Id);
                model.Depth = 1;
            }
            else
            {
                //移至新的节点下时，判断是否为原节点的子级点, 如果是，则不允许
                if (parent.Paths.Contains(model.Id.ToString()))
                {
                    throw new DbxException(EnumCode.不允许移至子节点);
                }

                model.Paths = string.Format("{0}{1}|", parent.Paths, model.Id);
                model.Depth = parent.Depth + 1;
            }
        }

        private void setUpdates(List<TInput> inputs)
        {
            if (inputs.IsEmpty())
                throw new DbxException(EnumCode.参数异常);

            var datas = EngineHelper.Map<List<TEntity>>(inputs);
            if (datas.IsEmpty())
                throw new DbxException(EnumCode.参数异常);

            var parentIds = datas.Select(x => x.Parent).ToList();
            var parents = repository.Find(0, x => parentIds.Contains(x.Id));
            if (parents.IsEmpty())
                return;

            datas.ForEach(input =>
            {
                var parent = parents.Where(p => StringHelper.IsEqual(StringHelper.Get(input.Parent), StringHelper.Get(p.Id))).FirstOrDefault();
                if (parent.IsNull())
                {
                    input.Paths = string.Format("{0}|", input.Id);
                    input.Depth = 1;
                }
                else
                {
                    //移至新的节点下时，判断是否为原节点的子级点, 如果是，则不允许
                    if (parent.Paths.Contains(input.Id.ToString()))
                    {
                        throw new DbxException(EnumCode.不允许移至子节点);
                    }

                    input.Paths = string.Format("{0}{1}|", parent.Paths, input.Id);
                    input.Depth = parent.Depth + 1;
                }
            });
        }

        private Expression<Func<TEntity, bool>> getDeletePredicate(TInput input)
        {
            if (input.IsNull())
                throw new DbxException(EnumCode.参数异常);

            var data = EngineHelper.Map<TEntity>(input);
            if (data.IsNull())
                throw new DbxException(EnumCode.参数异常);

            Expression<Func<TEntity, bool>> exp = null;
            exp = x => x.Paths.ToLower().Contains(data.Id.ToString().ToLower());
            return exp;
        }

        private Expression<Func<TEntity, bool>> getDeletePredicate(TId id)
        {
            if (id.IsNull())
                throw new DbxException(EnumCode.参数异常);

            Expression<Func<TEntity, bool>> exp = null;
            exp = x => x.Paths.ToLower().Contains(id.ToString().ToLower());
            return exp;
        }

        private List<Expression<Func<TEntity, bool>>> getDeletePredicate(List<TId> ids)
        {
            if (ids.IsNull())
                throw new DbxException(EnumCode.参数异常);

            List<Expression<Func<TEntity, bool>>> exps = new List<Expression<Func<TEntity, bool>>>();
            List<string> idArr = ids.Select(x => x.ToString().ToLower()).ToList();
            idArr.ForEach(s =>
            {
                Expression<Func<TEntity, bool>> exp = x => x.Paths.ToLower().Contains(s);
                exps.Add(exp);
            });
            return exps;
        }

        public TOutput Get(TId id)
        {
            throw new NotImplementedException();
        }

        public List<TOutput> GetIds(List<TId> ids)
        {
            throw new NotImplementedException();
        }

        public List<TOutput> Query(QueryInput query)
        {
            throw new NotImplementedException();
        }

        public PagingModel<TOutput> Paging(QueryInput query)
        {
            throw new NotImplementedException();
        }

        public List<TOutput> Find(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> Sorting = null)
        {
            throw new NotImplementedException();
        }

        public Task<TOutput> GetAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TOutput>> GetIdsAsync(List<TId> ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TOutput>> QueryAsync(QueryInput query)
        {
            throw new NotImplementedException();
        }

        public Task<PagingModel<TOutput>> PagingAsync(QueryInput query)
        {
            throw new NotImplementedException();
        }

        public Task<List<TOutput>> FindAsync(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> Sorting = null)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<TEntity, bool>> FindWhere(QueryInput queryParam)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
    

    /// <summary>
    /// 树结构业务操作
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ApplicationTree<TRep, TEntity, TInput, TOutput> : ApplicationTree<TRep, TEntity, Guid, TInput, TOutput>,
        IApplicationTree<TRep, TEntity, TInput, TOutput>
        where TEntity : class, IEntityTree<Guid>
        where TRep : IRepository<TEntity, Guid>
    {
        public ApplicationTree(TRep _repository) : base(_repository)
        {
        }
    }

    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationTree<TEntity, TInput, TOutput> : ApplicationTree<IRepository<TEntity, Guid>, TEntity, TInput, TOutput>,
        IApplicationTree<TEntity, TInput, TOutput>
        where TEntity : class, IEntityTree<Guid>
    {
        public ApplicationTree(IRepository<TEntity, Guid> _repository) : base(_repository)
        {
        }
    }


    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationTree<TRep, TEntity> : ApplicationTree<TRep, TEntity, TEntity, TEntity>,
        IApplicationTree<TRep, TEntity>
        where TEntity : class, IEntityTree<Guid>
        where TRep : IRepository<TEntity, Guid>
    {
        public ApplicationTree(TRep _repository) : base(_repository)
        {
        }
    }

    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationTree<TEntity> : ApplicationTree<IRepository<TEntity, Guid>, TEntity>,
        IApplicationTree<TEntity>
        where TEntity : class, IEntityTree<Guid>
    {

        public ApplicationTree(IRepository<TEntity, Guid> _repository) : base(_repository)
        {
        }
    }
}
