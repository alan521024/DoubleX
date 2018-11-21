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
    /// 应用服务基类实现
    /// 不抽象，可直接解析使用
    /// </summary>
    public class DomainDefaultService<TRepository, TEntity, TKey> :
        DomainService,
        IDomainDefaultService<TEntity, TKey>, IDomainService
        where TRepository : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly TRepository Repository;

        public DomainDefaultService(TRepository repository, IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {
            Repository = repository;
        }

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual TEntity Insert(TEntity entity)
        {
            entity = InsertBefore(new List<TEntity>() { entity }).FirstOrDefault();
            var rows = Repository.Insert(entity);
            entity = InsertAfter(new List<TEntity>() { entity }).FirstOrDefault();
            return rows > 0 ? entity : null;
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual List<TEntity> Insert(List<TEntity> list)
        {
            var entitys = InsertBefore(list);
            var rows = Repository.Insert(entitys);
            entitys = InsertAfter(entitys);
            return rows > 0 ? entitys : null;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity = InsertBefore(new List<TEntity>() { entity }).FirstOrDefault();
            var rows = await Repository.InsertAsync(entity);
            entity = InsertAfter(new List<TEntity>() { entity }).FirstOrDefault();
            return rows > 0 ? entity : null;
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> InsertAsync(List<TEntity> list)
        {
            var entitys = InsertBefore(list);
            var rows = await Repository.InsertAsync(entitys);
            entitys = InsertAfter(entitys);
            return rows > 0 ? entitys : null;
        }

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual TEntity Update(TEntity entity)
        {
            entity = UpdateBefore(new List<TEntity>() { entity }).FirstOrDefault();
            var rows = Repository.Update(entity);
            entity = UpdateAfter(new List<TEntity>() { entity }).FirstOrDefault();
            return rows > 0 ? entity : null;
        }

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual List<TEntity> Update(List<TEntity> list)
        {
            var entitys = UpdateBefore(list);
            var rows = Repository.Update(entitys);
            entitys = UpdateAfter(entitys);
            return rows > 0 ? entitys : null;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = UpdateBefore(new List<TEntity>() { entity }).FirstOrDefault();
            var rows = await Repository.UpdateAsync(entity);
            entity = UpdateAfter(new List<TEntity>() { entity }).FirstOrDefault();
            return rows > 0 ? entity : null;
        }

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> UpdateAsync(List<TEntity> list)
        {
            var entitys = UpdateBefore(list);
            var rows = await Repository.UpdateAsync(entitys);
            entitys = UpdateAfter(entitys);
            return rows > 0 ? entitys : null;
        }

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        public virtual int Delete(TKey id)
        {
            var ids = new List<TKey>() { id };
            DeleteBefore(ids);
            var rows = Repository.Delete(ids.FirstOrDefault());
            DeleteAfter(ids);
            return rows;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual int Delete(List<TKey> ids)
        {
            DeleteBefore(ids);
            var rows = Repository.Delete(ids);
            DeleteAfter(ids);
            return rows;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual int Delete(TEntity entity)
        {
            var ids = new List<TKey>() { entity.Id };
            DeleteBefore(ids);
            var rows = Repository.Delete(ids.FirstOrDefault());
            DeleteAfter(ids);
            return rows;
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual int Delete(List<TEntity> list)
        {
            var ids = list.Select(x => x.Id).ToList();
            DeleteBefore(ids);
            var rows = Repository.Delete(ids);
            DeleteAfter(ids);
            return rows;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        public virtual async Task<int> DeleteAsync(TKey id)
        {
            var ids = new List<TKey>() { id };
            DeleteBefore(ids);
            var result = await Repository.DeleteAsync(ids.FirstOrDefault());
            DeleteAfter(ids);
            return result;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual async Task<int> DeleteAsync(List<TKey> ids)
        {
            DeleteBefore(ids);
            var result = await Repository.DeleteAsync(ids);
            DeleteAfter(ids);
            return result;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            var ids = new List<TKey>() { entity.Id };
            DeleteBefore(ids);
            var result = await Repository.DeleteAsync(ids.FirstOrDefault());
            DeleteAfter(ids);
            return result;
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual async Task<int> DeleteAsync(List<TEntity> list)
        {
            var ids = list.Select(x => x.Id).ToList();
            DeleteBefore(ids);
            var result = await Repository.DeleteAsync(ids);
            DeleteAfter(ids);
            return result;
        }


        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual TEntity Get(TKey key)
        {
            return Repository.Get(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Repository.Get(where);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        public virtual List<TEntity> Find(int top = 0, Expression<Func<TEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            return Repository.Find(top: top, where: where, sorting: sorting);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        public virtual PagingModel<TEntity> Paging(int page, int size, Expression<Func<TEntity, bool>> where, List<KeyValueModel> sorting)
        {
            PagingModel<TEntity> result = new PagingModel<TEntity>();
            int total = 0;
            var rows = Repository.Paging(page, size, where, sorting, ref total);
            result.Rows = rows;
            result.Total = total;
            return result;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            return await Repository.GetAsync(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Repository.GetAsync(where);
        }

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        public virtual async Task<List<TEntity>> FindAsync(int top = 0, Expression<Func<TEntity, bool>> where = null, List<KeyValueModel> sorting = null)
        {
            return await Repository.FindAsync(top: top, where: where, sorting: sorting);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        public virtual async Task<PagingModel<TEntity>> PagingAsync(int page, int size, Expression<Func<TEntity, bool>> where, List<KeyValueModel> sorting)
        {
            throw new Exception("暂时不要用");
            //var result = Task<KeyValuePair<List<TEntity>, int>>;
            //var source = Repository.PagingAsync(page, size, where, sorting, total);
            //PagingModel<TEntity> result = new PagingModel<TEntity>();
            //var result = await Repository.PagingAsync(page, size, where, sorting, ref total);
            //result.Rows = rows;
            //result.Total = total;
            //return result;
        }

        #endregion

        #endregion

        #region 存在判断/函数

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> where)
        {
            return Repository.Any(where);
        }

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Repository.AnyAsync(where);
        }

        #endregion

        #region 取最大值/函数

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return Repository.Max<TResult>(field: field, name: name, where: where);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return await Repository.MaxAsync<TResult>(field: field, name: name, where: where);
        }

        #endregion

        #region 取最小值/函数

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return Repository.Min<TResult>(field: field, name: name, where: where);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return await Repository.MinAsync<TResult>(field: field, name: name, where: where);
        }

        #endregion

        #region 总数计算/函数

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> where = null)
        {
            return Repository.Count(where: where);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await Repository.CountAsync(where: where);
        }

        #endregion

        #region 求合计算/函数

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual TResult Sum<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return Repository.Sum(field: field, name: name, where: where);
        }

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return await Repository.SumAsync(field: field, name: name, where: where);
        }

        #endregion

        #region 平均计算/函数

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual TResult Avg<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return Repository.Avg(field: field, name: name, where: where);
        }

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> where = null)
        {
            return await Repository.AvgAsync(field: field, name: name, where: where);
        }

        #endregion

        #region 增改删回调

        protected virtual List<TEntity> InsertBefore(List<TEntity> inputs) { return inputs; }

        protected virtual List<TEntity> InsertAfter(List<TEntity> inputs) { return inputs; }

        protected virtual List<TEntity> UpdateBefore(List<TEntity> inputs) { return inputs; }

        protected virtual List<TEntity> UpdateAfter(List<TEntity> inputs) { return inputs; }

        protected virtual void DeleteBefore(List<TKey> ids) { }

        protected virtual void DeleteAfter(List<TKey> ids) { }

        #endregion
    }

    /// <summary>
    /// 应用服务基类实现
    /// 不抽象，可直接解析使用
    /// </summary>
    public class DomainDefaultService<TRepository, TEntity> :
        DomainDefaultService<TRepository, TEntity, Guid>,
        IDomainDefaultService<TEntity>, IDomainService
        where TRepository : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public DomainDefaultService(TRepository repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {
        }
    }

    /// <summary>
    /// 应用服务基类实现
    /// </summary>
    public class DomainDefaultService<TEntity> :
        DomainDefaultService<IRepository<TEntity>, TEntity>,
        IDomainDefaultService<TEntity>, IDomainService
        where TEntity : class, IEntity
    {

        public DomainDefaultService(IRepository<TEntity> repository, IApplicationSession session, ICachingService caching) :
            base(repository, session, caching)
        {

        }
    }

}