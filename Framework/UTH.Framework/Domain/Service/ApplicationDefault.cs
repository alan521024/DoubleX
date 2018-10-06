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
    /// 模块业务操作(仓储, 增，修改，删，获取，查询，分页)
    /// </summary>
    public class ApplicationDefault<TRep, TEntity, TId, TInput, TOutput> : ApplicationService, IApplicationDefault<TRep, TEntity, TId, TInput, TOutput>
        where TEntity : class, IEntity<TId>
        where TRep : IRepository<TEntity, TId>
    {
        public TRep repository { get; }

        public ApplicationDefault(TRep _repository)
        {
            repository = _repository;
        }

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        public virtual TOutput Insert(TInput input)
        {
            input.CheckNull();

            InsertBeforeCall?.Invoke(input);

            var entity = EngineHelper.Map<TEntity>(input);
            var output = default(TOutput);

            if (repository.Insert(entity) > 0)
            {
                output = EngineHelper.Map<TOutput>(entity);
            }

            if (!InsertAfterCall.IsNull())
            {
                output = InsertAfterCall(output);
            }

            return output;
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        public virtual List<TOutput> InsertBatch(List<TInput> inputs)
        {
            inputs.CheckNull();
            inputs.ForEach(input => InsertBeforeCall?.Invoke(input));

            var entitys = EngineHelper.Map<List<TEntity>>(inputs);
            var outputs = new List<TOutput>();

            if (repository.Insert(entitys) > 0)
            {
                outputs = EngineHelper.Map<List<TOutput>>(entitys);
            }

            if (!InsertAfterCall.IsNull())
            {
                for (var i = 0; i < outputs.Count; i++)
                {
                    outputs[i] = InsertAfterCall.Invoke(outputs[i]);
                }
            }

            return outputs;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        public virtual Task<TOutput> InsertAsync(TInput input)
        {
            return Task.FromResult(Insert(input));
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        public virtual Task<List<TOutput>> InsertBatchAsync(List<TInput> inputs)
        {
            return Task.FromResult(InsertBatch(inputs));
        }

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        public virtual TOutput Update(TInput input)
        {
            input.CheckNull();

            TEntity entity = EngineHelper.Map<TEntity>(input);
            TOutput output = default(TOutput);

            var update = input as IInputUpdate<TId>;
            if (!update.IsNull())
            {
                entity = repository.Find(update.Id);
            }

            if (!UpdateBeforeCall.IsNull())
            {
                entity = UpdateBeforeCall(input, entity);
            }

            if (repository.Update(entity) > 0)
            {
                output = EngineHelper.Map<TOutput>(entity);
            }

            if (!UpdateAfterCall.IsNull())
            {
                output = UpdateAfterCall(output);
            }

            return output;
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        public virtual List<TOutput> UpdateBatch(List<TInput> inputs)
        {
            inputs.CheckNull();

            var entities = EngineHelper.Map<List<TEntity>>(inputs);
            var outputs = new List<TOutput>();

            var upModes = new List<KeyValueModel<TId, TInput>>();
            inputs.ForEach(input =>
            {
                var model = input as IInputUpdate<TId>;
                if (!model.IsNull())
                {
                    upModes.Add(new KeyValueModel<TId, TInput>(model.Id, input));
                }
            });

            if (!upModes.IsEmpty())
            {
                var ids = upModes.Select(x => x.Key).ToList();
                entities = repository.Find(0, x => ids.Contains(x.Id));
            }
            else
            {
                entities = EngineHelper.Map<List<TEntity>>(inputs);
            }

            if (!UpdateBeforeCall.IsNull())
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var item = upModes.Where(x => StringHelper.IsEqual(x.Key.ToString(), entities[i].Id.ToString())).FirstOrDefault();
                    entities[i] = UpdateBeforeCall(item.IsNull() ? default(TInput) : item.Value, entities[i]);
                }
            }

            if (repository.Update(entities) > 0)
            {
                outputs = EngineHelper.Map<List<TOutput>>(entities);
            }

            if (!UpdateAfterCall.IsNull())
            {
                for (var i = 0; i < outputs.Count; i++)
                {
                    outputs[i] = UpdateAfterCall(outputs[i]);
                }
            }

            return outputs;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        public virtual Task<TOutput> UpdateAsync(TInput input)
        {
            return Task.FromResult(Update(input));
        }

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        public virtual Task<List<TOutput>> UpdateBatchAsync(List<TInput> inputs)
        {
            return Task.FromResult(UpdateBatch(inputs));
        }

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除
        /// </summary>
        public virtual int Delete(TInput input)
        {
            var deleteInput = input as IInputDelete<TId>;
            var count = 0;
            if (!deleteInput.Id.IsEmpty())
            {
                count = repository.Delete(deleteInput.Id);
            }
            if (!deleteInput.Ids.IsEmpty())
            {
                count = repository.Delete(deleteInput.Ids);
            }
            return count;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual int DeleteId(TId id)
        {
            return repository.Delete(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual int DeleteIds(List<TId> ids)
        {
            return repository.Delete(ids);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 删除
        /// </summary>
        public virtual Task<int> DeleteAsync(TInput input)
        {
            var deleteInput = input as IInputDelete<TId>;
            var count = 0;
            if (!deleteInput.Id.IsEmpty())
            {
                count = repository.Delete(deleteInput.Id);
            }
            if (!deleteInput.Ids.IsEmpty())
            {
                count = repository.Delete(deleteInput.Ids);
            }
            return Task.FromResult<int>(count);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual Task<int> DeleteIdAsync(TId id)
        {
            return repository.DeleteAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual Task<int> DeleteIdsAsync(List<TId> ids)
        {
            return repository.DeleteAsync(ids);
        }

        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        public virtual TOutput Get(TId id)
        {
            var entity = repository.Find(id);
            return EngineHelper.Map<TOutput>(entity);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        public virtual List<TOutput> GetIds(List<TId> ids)
        {
            var entitys = repository.Find(x => ids.Contains(x.Id));
            return EngineHelper.Map<List<TOutput>>(entitys);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        public virtual List<TOutput> Query(QueryInput query)
        {
            Expression<Func<TEntity, bool>> predicate = FindWhere(query);

            var entitys = repository.Find(query.Size, predicate: predicate, sorting: query.Sorting);
            return EngineHelper.Map<List<TOutput>>(entitys);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public virtual PagingModel<TOutput> Paging(QueryInput query)
        {
            Expression<Func<TEntity, bool>> predicate = FindWhere(query);

            PagingModel<TOutput> result = new PagingModel<TOutput>();
            var total = 0L;
            var rows = repository.Find(query.Page, query.Size, predicate, query.Sorting, out total);
            result.Rows = total > 0 ? EngineHelper.Map<List<TOutput>>(rows) : new List<TOutput>();
            result.Total = total;
            return result;
        }

        /// <summary>
        /// 查找集合
        /// </summary>
        public virtual List<TOutput> Find(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> Sorting = null)
        {
            var entitys = repository.Find(top, predicate: predicate, sorting: Sorting);
            return EngineHelper.Map<List<TOutput>>(entitys);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        public virtual Task<TOutput> GetAsync(TId id)
        {
            var entity = repository.Find(id);
            return Task.FromResult(EngineHelper.Map<TOutput>(entity));
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        public virtual Task<List<TOutput>> GetIdsAsync(List<TId> ids)
        {
            var entitys = repository.FindAsync(x => ids.Contains(x.Id));
            return Task.FromResult(EngineHelper.Map<List<TOutput>>(entitys));
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        public virtual Task<List<TOutput>> QueryAsync(QueryInput query)
        {
            Expression<Func<TEntity, bool>> predicate = FindWhere(query);

            var entitys = repository.Find(query.Size, predicate: predicate, sorting: query.Sorting);
            return Task.FromResult(EngineHelper.Map<List<TOutput>>(entitys));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public virtual Task<PagingModel<TOutput>> PagingAsync(QueryInput query)
        {
            Expression<Func<TEntity, bool>> predicate = FindWhere(query);

            PagingModel<TOutput> result = new PagingModel<TOutput>();
            var total = 0L;
            var rows = repository.Find(query.Page, query.Size, predicate, query.Sorting, out total);
            result.Rows = total > 0 ? EngineHelper.Map<List<TOutput>>(rows) : new List<TOutput>();
            result.Total = total;
            return Task.FromResult(result);
        }

        /// <summary>
        /// 查找集合
        /// </summary>
        public virtual Task<List<TOutput>> FindAsync(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> Sorting = null)
        {
            var entitys = repository.Find(top == -1 ? 0 : top, predicate: predicate, sorting: Sorting);
            return Task.FromResult(EngineHelper.Map<List<TOutput>>(entitys));
        }

        #endregion

        #endregion

        #region 新增/更新/删除/查询 回调

        public virtual Action<TInput> InsertBeforeCall { get; } = null;
        public virtual Func<TOutput, TOutput> InsertAfterCall { get; } = null;

        public virtual Func<TInput, TEntity, TEntity> UpdateBeforeCall { get; } = null;
        public virtual Func<TOutput, TOutput> UpdateAfterCall { get; } = null;

        public virtual Expression<Func<TEntity, bool>> FindWhere(QueryInput queryParam) { return null; }

        #endregion

        #region 业务操作

        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="session"></param>
        public override void SetSession(IApplicationSession session)
        {
            Session = session;
            repository?.SetSession(session);
        }

        #endregion
    }


    /// <summary>
    /// 模块业务操作
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ApplicationDefault<TRep, TEntity, TInput, TOutput> : ApplicationDefault<TRep, TEntity, Guid, TInput, TOutput>,
        IApplicationDefault<TRep, TEntity, TInput, TOutput>
        where TEntity : class, IEntity<Guid>
        where TRep : IRepository<TEntity, Guid>
    {
        public ApplicationDefault(TRep _repository) : base(_repository)
        {
        }
    }

    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationDefault<TEntity, TInput, TOutput> : ApplicationDefault<IRepository<TEntity, Guid>, TEntity, TInput, TOutput>,
        IApplicationDefault<TEntity, TInput, TOutput>
        where TEntity : class, IEntity<Guid>
    {
        public ApplicationDefault(IRepository<TEntity, Guid> _repository) : base(_repository)
        {
        }
    }


    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationDefault<TRep, TEntity> : ApplicationDefault<TRep, TEntity, TEntity, TEntity>,
        IApplicationDefault<TRep, TEntity>
        where TEntity : class, IEntity<Guid>
        where TRep : IRepository<TEntity, Guid>
    {
        public ApplicationDefault(TRep _repository) : base(_repository)
        {
        }
    }

    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationDefault<TEntity> : ApplicationDefault<IRepository<TEntity, Guid>, TEntity>,
        IApplicationDefault<TEntity>
        where TEntity : class, IEntity<Guid>
    {

        public ApplicationDefault(IRepository<TEntity, Guid> _repository) : base(_repository)
        {
        }
    }

}
