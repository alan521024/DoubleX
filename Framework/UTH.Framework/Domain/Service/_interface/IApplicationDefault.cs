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
    /// 模块业务操作接口(仓储, 增，修改，删，获取，查询，分页)
    /// </summary>
    public interface IApplicationDefault<TRep, TEntity, TId, TInput, TOutput> : IApplicationService
        where TEntity : class, IEntity<TId>
        where TRep : IRepository<TEntity, TId>
    {
        #region 公共属性

        TRep repository { get; }

        #endregion

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        TOutput Insert(TInput input);

        /// <summary>
        /// 添加集合
        /// </summary>
        List<TOutput> InsertBatch(List<TInput> inputs);

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        Task<TOutput> InsertAsync(TInput input);

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        Task<List<TOutput>> InsertBatchAsync(List<TInput> inputs);

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        TOutput Update(TInput input);

        /// <summary>
        /// 修改对象
        /// </summary>
        List<TOutput> UpdateBatch(List<TInput> inputs);

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        Task<TOutput> UpdateAsync(TInput input);

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        Task<List<TOutput>> UpdateBatchAsync(List<TInput> inputs);

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除
        /// </summary>
        int Delete(TInput input);

        /// <summary>
        /// 删除
        /// </summary>
        int DeleteId(TId key);

        /// <summary>
        /// 删除
        /// </summary>
        int DeleteIds(List<TId> ids);

        #region 异步(可等待)操作

        /// <summary>
        /// 删除keys
        /// </summary>
        Task<int> DeleteAsync(TInput input);

        /// <summary>
        /// 删除key
        /// </summary>
        Task<int> DeleteIdAsync(TId key);

        /// <summary>
        /// 删除keys
        /// </summary>
        Task<int> DeleteIdsAsync(List<TId> ids);

        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        TOutput Get(TId id);

        /// <summary>
        /// 获取集合
        /// </summary>
        List<TOutput> GetIds(List<TId> ids);

        /// <summary>
        /// 查询集合
        /// </summary>
        List<TOutput> Query(QueryInput query);

        /// <summary>
        /// 查找集合
        /// </summary>
        List<TOutput> Query(int top = 0, Expression<Func<TEntity, bool>> where = null, List<KeyValueModel> Sorting = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        PagingModel<TOutput> Paging(QueryInput query);

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        Task<TOutput> GetAsync(TId id);

        /// <summary>
        /// 获取对象
        /// </summary>
        Task<List<TOutput>> GetIdsAsync(List<TId> ids);

        /// <summary>
        /// 获取集合
        /// </summary>
        Task<List<TOutput>> QueryAsync(QueryInput query);

        /// <summary>
        /// 查找集合
        /// </summary>
        Task<List<TOutput>> QueryAsync(int top = 0, Expression<Func<TEntity, bool>> where = null, List<KeyValueModel> Sorting = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PagingModel<TOutput>> PagingAsync(QueryInput query);

        #endregion

        #endregion

        #region 新增/更新/删除/查询 回调

        Action<TInput> InsertBeforeCall { get; }
        Func<TOutput, TOutput> InsertAfterCall { get; }

        Func<TInput, TEntity, TEntity> UpdateBeforeCall { get; }
        Func<TOutput, TOutput> UpdateAfterCall { get; }

        Expression<Func<TEntity, bool>> FindWhere(QueryInput input);

        #endregion
    }


    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IApplicationDefault<TRep, TEntity, TInput, TOutput> : IApplicationDefault<TRep, TEntity, Guid, TInput, TOutput>
        where TEntity : class, IEntity<Guid>
        where TRep : IRepository<TEntity, Guid>
    {

    }

    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationDefault<TEntity, TInput, TOutput> : IApplicationDefault<IRepository<TEntity, Guid>, TEntity, TInput, TOutput>
        where TEntity : class, IEntity<Guid>
    {

    }


    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationDefault<TRep, TEntity> : IApplicationDefault<TRep, TEntity, TEntity, TEntity>
        where TEntity : class, IEntity<Guid>
        where TRep : IRepository<TEntity, Guid>
    {

    }

    /// <summary>
    /// 模块业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationDefault<TEntity> : IApplicationDefault<IRepository<TEntity, Guid>, TEntity>
        where TEntity : class, IEntity<Guid>
    {

    }

}
