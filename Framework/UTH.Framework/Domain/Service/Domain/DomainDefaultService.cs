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

    /// <summary>
    /// 应用服务基类实现
    /// 不抽象，可直接解析使用
    /// </summary>
    public class DomainDefaultService<TRepository, TEntity> :
        DomainService,
        IDomainDefaultService<TEntity>, IDomainService
        where TRepository : IRepository<TEntity>
        where TEntity : class, IEntity
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
        public virtual int Insert(TEntity entity)
        {
            return Repository.Insert(entity);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual int Insert(List<TEntity> list)
        {
            return Repository.Insert(list);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            return await Repository.InsertAsync(entity);
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(List<TEntity> list)
        {
            return await Repository.InsertAsync(list);
        }

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual int Update(TEntity entity)
        {
            return Repository.Update(entity);
        }

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual int Update(List<TEntity> list)
        {
            return Repository.Update(list);
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null)
        {
            return Repository.Update(where: where, columns: columns, setValueExpression: setValueExpression, entity: entity);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            return await Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(List<TEntity> list)
        {
            return await Repository.UpdateAsync(list);
        }

        /// <summary>
        /// 修改操作-(可等待)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null)
        {
            return await Repository.UpdateAsync(where: where, columns: columns, setValueExpression: setValueExpression, entity: entity);
        }

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        public virtual int Delete(Guid id)
        {
            return Repository.Delete(id);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual int Delete(List<Guid> ids)
        {
            return Repository.Delete(ids);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual int Delete(TEntity entity)
        {
            return Repository.Delete(entity);
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual int Delete(Expression<Func<TEntity, bool>> where)
        {
            return Repository.Delete(where);
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual int Delete(List<TEntity> list)
        {
            return Repository.Delete(list);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual async Task<int> DeleteAsync(List<Guid> ids)
        {
            return await Repository.DeleteAsync(ids);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            return await Repository.DeleteAsync(entity);
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Repository.DeleteAsync(where);
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        public virtual async Task<int> DeleteAsync(List<TEntity> list)
        {
            return await Repository.DeleteAsync(list);
        }


        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual TEntity Get(Guid key)
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
        public virtual async Task<TEntity> GetAsync(Guid key)
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
    }

}