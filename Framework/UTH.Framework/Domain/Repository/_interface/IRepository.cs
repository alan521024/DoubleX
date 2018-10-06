namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data.Common;
    using System.Data;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 仓储默认接口
    /// </summary>
    public interface IRepository : IDependency
    {
        #region 公共属性

        /// <summary>
        /// 连接信息
        /// </summary>
        ConnectionModel Connection { get; }

        /// <summary>
        /// 访问会话
        /// </summary>
        IApplicationSession Session { get; }

        #endregion

        #region 脚本执行

        /// <summary>
        /// 脚本执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int SqlExecuteCommand(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 脚本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        dynamic SqlQueryDynamic(string sql, params DbParameter[] parameters);

        #endregion

        #region 事务操作

        /// <summary>
        /// 事务仓储参数对象
        /// </summary>
        KeyValueModel<string, object>[] TranParams { get; }

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran(IsolationLevel? iso = null, string transactionName = null);

        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 事务提交
        /// </summary>
        void CommitTran();


        /// <summary>
        /// 事务操作
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        bool UseTransaction(Action<IRepository> action);

        /// <summary>
        /// 事务操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        T UseTransaction<T>(Func<T> func);

        #endregion

        #region 仓储操作

        /// <summary>
        /// 获取仓储连接对象
        /// </summary>
        /// <returns></returns>
        object GetClient();

        /// <summary>
        /// 获取仓储连接对象
        /// </summary>
        /// <returns></returns>
        T GetClient<T>() where T : class;

        /// <summary>
        /// 设置仓储连接对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void SetClient<T>(T t) where T : class;

        #endregion

        #region 会话操作

        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="session"></param>
        void SetSession(IApplicationSession session);

        #endregion
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class, IEntity<TKey>
    {
        /****
        //仓储实现构造函数 可参考 SqlSugarRepository
        //public SqlSugarRepository(string connectionStr = null, ConnectionModel connectionModel = null, SqlSugarClient connectionClient = null) { }
        //不建义分类写public SqlSugarRepository(string connectionStr) { },public SqlSugarRepository(ConnectionModel connectionModel) { },………………
        //Autofac解析会导致参数不能在具有相同长度1的多个构造函数之间进行选择(未详细测试)
        //
        //初始Ioc注册实，设置默认connectionModel(connectionStr/connectionClient 为空 null)
        //例：
        //Parameters = new List<KeyValueModel<string, object>>(){
        //    new KeyValueModel<string, object>("connectionStr",null),
        //    new KeyValueModel<string, object>("connectionModel", repositoryConnection),
        //    new KeyValueModel<string, object>("connectionClient", null)
        //}
        //Service业务类中，仓储事务操作可传入，Server的主仓储获取到的 KeyValueModel<string, object> ClientParams { get; } [防止事务不能跨连接对象]
        //
        **/

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Insert(TEntity entity);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int Insert(List<TEntity> list);

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> InsertAsync(List<TEntity> list);

        #endregion

        #endregion

        #region 修改对象/集合

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Update(TEntity entity);

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int Update(List<TEntity> list);

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null);

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(List<TEntity> list);

        /// <summary>
        /// 修改操作-(可等待)
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null);

        #endregion

        #endregion

        #region 删除对象/集合

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        int Delete(TKey id);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        int Delete(List<TKey> ids);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Delete(TEntity entity);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        int Delete(List<TEntity> list);

        #region 异步(可等待)操作

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        Task<int> DeleteAsync(List<TKey> ids);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        Task<int> DeleteAsync(List<TEntity> list);


        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        TEntity Find(TKey key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        List<TEntity> Find(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> sorting = null);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        List<TEntity> Paging(int page, int size, Expression<Func<TEntity, bool>> predicate, List<KeyValueModel> sorting, ref int total);

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        Task<List<TEntity>> FindAsync(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> sorting = null);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        Task<KeyValuePair<List<TEntity>, int>> PagingAsync(int page, int size, Expression<Func<TEntity, bool>> predicate, List<KeyValueModel> sorting, int total);

        #endregion

        #endregion


        #region 存在判断/函数

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        #endregion

        #region 取最大值/函数

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region 取最小值/函数

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region 总数计算/函数

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region 求合计算/函数

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        TResult Sum<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region 平均计算/函数

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        TResult Avg<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity
    {
    }
}
