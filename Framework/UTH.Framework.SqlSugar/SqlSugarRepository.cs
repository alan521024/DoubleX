namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using SqlSugar;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// SqlSugar 仓储
    /// </summary>
    public class SqlSugarRepository : IRepository
    {
        #region 私有变量

        protected SqlSugarClient client;

        #endregion

        #region 公共属性

        /// <summary>
        /// 连接信息
        /// </summary>
        public ConnectionModel Connection { get; protected set; }

        /// <summary>
        /// 访问会话信息
        /// </summary>
        public IApplicationSession Session { get; protected set; }

        #endregion

        #region 脚本执行

        /// <summary>
        /// 脚本执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int SqlExecuteCommand(string sql, params DbParameter[] parameters)
        {
            try
            {
                return client.Ado.ExecuteCommand(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 脚本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual dynamic SqlQueryDynamic(string sql, params DbParameter[] parameters)
        {
            try
            {
                return client.Ado.SqlQueryDynamic(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库查询异常, ex);
            }
        }

        #endregion

        #region 事务操作

        /// <summary>
        /// 事务仓储参数对象
        /// </summary>
        public KeyValueModel<string, object>[] TranParams
        {
            get
            {
                return new KeyValueModel<string, object>[] {
                    new KeyValueModel<string, object>("connectionStr",null),
                    new KeyValueModel<string, object>("connectionModel", null),
                    new KeyValueModel<string, object>("connectionClient", client)
                };
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="iso"></param>
        /// <param name="transactionName"></param>
        public virtual void BeginTran(IsolationLevel? iso = null, string transactionName = null)
        {
            if (iso.IsNull() && transactionName.IsEmpty())
            {
                client.Ado.BeginTran();
                return;
            }

            if (!iso.IsNull())
            {
                client.Ado.BeginTran(iso.Value);
                return;
            }

            if (!transactionName.IsEmpty())
            {
                client.Ado.BeginTran(transactionName);
                return;
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public virtual void RollbackTran()
        {
            client.Ado.RollbackTran();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public virtual void CommitTran()
        {
            client.Ado.CommitTran();
        }

        /// <summary>
        /// 事务操作
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual bool UseTransaction(Action<IRepository> action)
        {
            DbResult<bool> result = client.Ado.UseTran(() => action(this));
            if (result != null)
            {
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else if (!result.ErrorMessage.IsEmpty())
                {
                    throw new DbxException(EnumCode.数据库事务异常, result.ErrorMessage);
                }
                else if (!result.ErrorException.IsNull())
                {
                    throw new DbxException(EnumCode.数据库事务异常, result.ErrorException);
                }
            }
            return false;
        }

        /// <summary>
        /// 事务操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual T UseTransaction<T>(Func<T> func)
        {
            DbResult<T> result = client.Ado.UseTran<T>(func);
            if (result != null)
            {
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else if (!result.ErrorMessage.IsEmpty())
                {
                    throw new DbxException(EnumCode.数据库事务异常, result.ErrorMessage);
                }
                else if (!result.ErrorException.IsNull())
                {
                    throw new DbxException(EnumCode.数据库事务异常, result.ErrorException);
                }
            }
            return default(T);
        }

        #endregion

        #region 仓储操作

        /// <summary>
        /// 获取仓储连接对象
        /// </summary>
        /// <returns></returns>
        public virtual object GetClient()
        {
            return client;
        }

        /// <summary>
        /// 获取仓储连接对象
        /// </summary>
        /// <returns></returns>
        public virtual T GetClient<T>() where T : class
        {
            return client as T;
        }

        /// <summary>
        /// 设置仓储连接对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public virtual void SetClient<T>(T t) where T : class
        {
            client = t as SqlSugarClient;
        }
        #endregion

        #region 会话操作

        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="session"></param>
        public virtual void SetSession(IApplicationSession session)
        {
            Session = session;
        }

        #endregion
    }

    /// <summary>
    /// SqlSugar 仓储
    /// </summary>
    public class SqlSugarRepository<TEntity, TKey> : SqlSugarRepository, IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        public SqlSugarRepository(string connectionStr = null, ConnectionModel connectionModel = null, SqlSugarClient connectionClient = null, IApplicationSession session = null)
        {
            if (!connectionClient.IsNull())
            {
                client = connectionClient;
                Connection = new ConnectionModel()
                {
                    ConnectionString = client.CurrentConnectionConfig.ConnectionString,
                    DbType = ResDBType(client.CurrentConnectionConfig.DbType),
                    AutoCloseConnection = client.CurrentConnectionConfig.IsAutoCloseConnection
                };
            }
            else if (!connectionModel.IsNull())
            {
                Connection = connectionModel;
                client = CreateClient(Connection);

            }
            else if (!connectionStr.IsEmpty())
            {
                Connection = new ConnectionModel(connectionStr);
                client = CreateClient(Connection);
            }
            else
            {
                throw new Exception("SqlSugarRepository Params is null");
            }

            Session = !session.IsNull() ? session : EngineHelper.Resolve<IApplicationSession>();
        }

        #region 私有变量

        protected bool isDataCache { get; set; }

        protected virtual Func<TEntity, TEntity> FindItemFunc { get; set; }

        protected virtual Func<List<TEntity>, List<TEntity>> FindListFunc { get; set; }

        #endregion

        #region 辅助操作

        private SqlSugarClient CreateClient(ConnectionModel config)
        {
            var sqlSugarConfig = new ConnectionConfig()
            {
                ConnectionString = config.GetConnectionString(),
                IsAutoCloseConnection = config.AutoCloseConnection,
                DbType = ToDBType(config.DbType),
                //IsShardSameThread = true //设为true相同线程是同一个SqlSugarClient http://www.codeisbug.com/Doc/8/1158
            };

            var model = new SqlSugarClient(sqlSugarConfig);

            if (EngineHelper.Configuration.IsDebugger)
            {
                model.Ado.IsEnableLogEvent = true;
                model.Ado.LogEventStarting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" + model.Utilities.SerializeObject(pars.ToDictionary(s => s.ParameterName, s => s.Value)));
                    Console.WriteLine();
                };
            }

            //过滤器：在关联查询时，条件只针对主体信息，，

            //过滤器-软删除
            if (typeof(TEntity).IsBaseFrom<ISoftDeleteEntity>())
            {
                if (model.QueryFilter.GeFilterList.Where(x => x.FilterName == "Audit.IsDelete").Count() == 0)
                {
                    model.QueryFilter.Add(new SqlFilterItem()
                    {
                        FilterName = "Audit.IsDelete",
                        FilterValue = filterDb => { return new SqlFilterResult() { Sql = " IsDelete=0 " }; },
                        IsJoinQuery = false
                    });
                }
                if (model.QueryFilter.GeFilterList.Where(x => x.FilterName == "Audit.IsDelete.F").Count() == 0)
                {
                    model.QueryFilter.Add(new SqlFilterItem()
                    {
                        FilterName = "Audit.IsDelete.F",
                        FilterValue = filterDb => { return new SqlFilterResult() { Sql = " f.IsDelete=0 " }; },
                        IsJoinQuery = true
                    });
                }
            }

            //过滤器-多租户
            if (typeof(TEntity).IsBaseFrom<ITenantEntity>())
            {
                //TODO:开启多租户
                //model.QueryFilter
                //    .Add(new SqlFilterItem()
                //    {
                //        FilterValue = filterDb => { return new SqlFilterResult() { Sql = " TenantId='610AB21D-02AA-40ED-9037-01AB06D88638' " }; },
                //        IsJoinQuery = false
                //    })
                //    .Add(new SqlFilterItem()
                //    {
                //        FilterValue = filterDb => { return new SqlFilterResult() { Sql = " f.TenantId='610AB21D-02AA-40ED-9037-01AB06D88638' " }; },
                //        IsJoinQuery = true
                //    });
            }

            return model;
        }

        private OrderByType ConvertOrderType(string value)
        {
            if (value.IsEmpty())
                return OrderByType.Asc;
            value = value.ToLower().Trim();
            if (value == "desc" || value == "1")
                return OrderByType.Desc;

            return OrderByType.Asc;
        }

        private SqlSugar.DbType ToDBType(EnumDbType dbType)
        {
            switch (dbType)
            {
                case EnumDbType.SqlServer:
                    return SqlSugar.DbType.SqlServer;
                case EnumDbType.MySql:
                    return SqlSugar.DbType.MySql;
                case EnumDbType.Oracle:
                    return SqlSugar.DbType.Oracle;
                case EnumDbType.Sqlite:
                    return SqlSugar.DbType.Sqlite;
                default:
                    return SqlSugar.DbType.SqlServer;
            }
        }

        private EnumDbType ResDBType(SqlSugar.DbType dbType)
        {
            switch (dbType)
            {
                case SqlSugar.DbType.SqlServer:
                    return EnumDbType.SqlServer;
                case SqlSugar.DbType.MySql:
                    return EnumDbType.MySql;
                case SqlSugar.DbType.Oracle:
                    return EnumDbType.Oracle;
                case SqlSugar.DbType.Sqlite:
                    return EnumDbType.Sqlite;
                default:
                    return EnumDbType.SqlServer;
            }
        }



        private ISugarQueryable<TEntity> GetSource()
        {
            var query = client.Queryable<TEntity>();
            if (isDataCache)
            {
                query = query.WithCache();
            }
            return query;
        }

        private IInsertable<TEntity> GetInsertable(TEntity entity)
        {
            var query = client.Insertable<TEntity>(entity);
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }

        private IInsertable<TEntity> GetInsertable(List<TEntity> list)
        {
            var query = client.Insertable<TEntity>(list);
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }


        private IUpdateable<TEntity> GetUpdateable(TEntity entity = null)
        {
            var query = client.Updateable<TEntity>(entity);
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }
        private IUpdateable<TEntity> GetUpdateable(List<TEntity> list)
        {
            var query = client.Updateable<TEntity>(list);
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }


        private IDeleteable<TEntity> GetDeleteable(TEntity entity = null)
        {
            IDeleteable<TEntity> query = null;
            if (entity == null)
            {
                query = client.Deleteable<TEntity>();
            }
            else
            {
                query = client.Deleteable<TEntity>(entity);
            }
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }
        private IDeleteable<TEntity> GetDeleteable(List<TEntity> list)
        {
            var query = client.Deleteable<TEntity>(list);
            if (isDataCache)
            {
                query = query.RemoveDataCache();
            }
            return query;
        }

        private void SetOperate<T>(List<T> list, EnumOperateType operate)
        {
            if (!list.IsEmpty())
            {
                list.ForEach(x =>
                {
                    SetOperate<T>(x, operate);
                });
            }
        }
        private void SetOperate<T>(T entity, EnumOperateType operate)
        {
            if (entity is T)
            {
                var auditedItem = entity as IAuditedEntity;
                if (auditedItem != null)
                {
                    switch (operate)
                    {
                        case EnumOperateType.Insert:
                            auditedItem.CreateDt = auditedItem.CreateDt.IsEmpty() ? DateTime.Now : auditedItem.CreateDt;
                            auditedItem.CreateId = !Session.IsNull() ? Session.AccountId : Guid.Empty;
                            auditedItem.LastDt = auditedItem.LastDt.IsEmpty() ? DateTime.Now : auditedItem.LastDt;
                            auditedItem.LastId = !Session.IsNull() ? Session.AccountId : Guid.Empty;
                            break;
                        case EnumOperateType.Update:
                        case EnumOperateType.Delete:
                            auditedItem.LastDt = DateTime.Now; //auditedItem.LastDt.IsEmpty() ?  : auditedItem.LastDt;
                            auditedItem.LastId = !Session.IsNull() ? Session.AccountId : Guid.Empty;
                            break;
                    }
                }

                var tenantItem = entity as ITenantEntity;
                if (tenantItem != null)
                {
                    switch (operate)
                    {
                        case EnumOperateType.Insert:
                            tenantItem.TenantId = !Session.IsNull() ? Session.TenantId : Guid.Empty;
                            break;
                        case EnumOperateType.Update:
                        case EnumOperateType.Delete:
                            //...
                            break;
                    }
                }
            }
        }


        protected ISugarQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> sorting = null)
        {
            var query = client.Queryable<TEntity>();

            if (isDataCache)
            {
                query = query.WithCache();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (sorting != null && sorting.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                sorting.ForEach((o) =>
                {
                    builder.AppendFormat(" {0} {1},", o.Key, ConvertOrderType(o.Value).GetName().ToUpper());
                });
                query = query.OrderBy(builder.TrimEnd(',').ToString());
            }

            return query;
        }


        #endregion

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual int Insert(TEntity entity)
        {
            try
            {
                SetOperate(entity, EnumOperateType.Insert);
                return GetInsertable(entity).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual int Insert(List<TEntity> list)
        {
            try
            {
                SetOperate<TEntity>(list, EnumOperateType.Insert);
                return GetInsertable(list).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                SetOperate<TEntity>(entity, EnumOperateType.Insert);
                return await GetInsertable(entity).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(List<TEntity> list)
        {
            try
            {
                SetOperate<TEntity>(list, EnumOperateType.Insert);
                return await GetInsertable(list).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
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
            try
            {
                SetOperate<TEntity>(entity, EnumOperateType.Update);
                return GetUpdateable(entity).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual int Update(List<TEntity> list)
        {
            try
            {
                SetOperate<TEntity>(list, EnumOperateType.Update);
                return GetUpdateable(list).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null)
        {
            IUpdateable<TEntity> updater = null;

            if (entity != null)
            {
                updater = GetUpdateable(entity);
            }
            else
            {
                updater = GetUpdateable();
            }

            if (predicate != null)
            {
                updater = updater.Where(predicate);
            }

            if (columns != null)
            {
                updater = updater.UpdateColumns(columns);
            }

            if (setValueExpression != null)
            {
                updater = updater.ReSetValue(setValueExpression);

            }

            try
            {
                return updater.ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 修改对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                SetOperate<TEntity>(entity, EnumOperateType.Update);
                return await GetUpdateable(entity).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 修改集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(List<TEntity> list)
        {
            try
            {
                SetOperate<TEntity>(list, EnumOperateType.Update);
                return await GetUpdateable(list).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 修改操作-(可等待)
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="columns">修改例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null)
        {
            IUpdateable<TEntity> updater = null;

            if (entity != null)
            {
                updater = GetUpdateable(entity);
            }
            else
            {
                updater = GetUpdateable();
            }

            if (predicate != null)
            {
                updater = updater.Where(predicate);
            }

            if (columns != null)
            {
                updater = updater.UpdateColumns(columns);
            }

            if (setValueExpression != null)
            {
                updater = updater.ReSetValue(setValueExpression);

            }

            try
            {
                return await updater.ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
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
            try
            {
                return GetDeleteable().In<TKey>(id).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual int Delete(List<TKey> ids)
        {
            try
            {
                return GetDeleteable().In<TKey>(ids).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual int Delete(TEntity entity)
        {
            try
            {
                return GetDeleteable(entity).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return GetDeleteable().Where(predicate).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        public virtual int Delete(List<TEntity> list)
        {
            try
            {
                return GetDeleteable(list).ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        #region 异步(可等待)操作


        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        public virtual async Task<int> DeleteAsync(TKey id)
        {
            try
            {
                return await GetDeleteable().In<TKey>(id).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        public virtual async Task<int> DeleteAsync(List<TKey> ids)
        {
            try
            {
                return await GetDeleteable().In<TKey>(ids).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                return await GetDeleteable(entity).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await GetDeleteable().Where(predicate).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        public virtual async Task<int> DeleteAsync(List<TEntity> list)
        {
            try
            {
                return await GetDeleteable(list).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw new DbxException(EnumCode.数据库执行异常, ex);
            }
        }


        #endregion

        #endregion

        #region 获取对象/集合

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual TEntity Find(TKey key)
        {
            var entity = GetQueryable().InSingle(key);
            FindItemFunc?.Invoke(entity);
            return entity;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = GetQueryable(predicate: predicate).First();  //超过1条,使用Single会报错，First不会报错
            FindItemFunc?.Invoke(entity);
            return entity;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        public virtual List<TEntity> Find(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> sorting = null)
        {
            ISugarQueryable<TEntity> query = GetQueryable(predicate: predicate, sorting: sorting);

            if (top > 0)
            {
                query = query.Take(top);
            }

            var list = query.ToList();

            FindListFunc?.Invoke(list);

            return list;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        public virtual List<TEntity> Paging(int page, int size, Expression<Func<TEntity, bool>> predicate, List<KeyValueModel> sorting, ref int total)
        {
            var list = GetQueryable(predicate: predicate, sorting: sorting).ToPageList(page, size, ref total);

            FindListFunc?.Invoke(list);

            return list;
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetQueryable(predicate: predicate).FirstAsync();  //超过1条,使用Single会报错，First不会报错
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        public virtual async Task<List<TEntity>> FindAsync(int top = 0, Expression<Func<TEntity, bool>> predicate = null, List<KeyValueModel> sorting = null)
        {
            ISugarQueryable<TEntity> query = GetQueryable(predicate: predicate, sorting: sorting);

            if (top > 0)
            {
                query = query.Take(top);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="predicate">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 new List[TEntity]</returns>
        public virtual async Task<KeyValuePair<List<TEntity>, int>> PagingAsync(int page, int size, Expression<Func<TEntity, bool>> predicate, List<KeyValueModel> sorting, int total)
        {
            return await GetQueryable(predicate: predicate, sorting: sorting).ToPageListAsync(page, size, total);
        }

        #endregion

        #endregion


        #region 存在判断/函数

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return GetSource().Where(expression).Any();
        }

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetSource().Where(expression).AnyAsync();
        }

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
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return source.Max<TResult>(field);
            }
            else
            {
                return source.Max<TResult>(name);
            }
        }


        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return await source.MaxAsync<TResult>(field);
            }
            else
            {
                return await source.MaxAsync<TResult>(name);
            }
        }

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
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return source.Min<TResult>(field);
            }
            else
            {
                return source.Min<TResult>(name);
            }
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return await source.MinAsync<TResult>(field);
            }
            else
            {
                return await source.MinAsync<TResult>(name);
            }
        }

        #endregion

        #region 总数计算/函数

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate.IsNull() ? GetSource().Count() : GetSource().Count(predicate);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await (predicate.IsNull() ? GetSource().CountAsync() : GetSource().CountAsync(predicate));
        }

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
        public virtual TResult Sum<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return source.Sum<TResult>(field);
            }
            else
            {
                return source.Sum<TResult>(name);
            }
        }

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return await source.SumAsync<TResult>(field);
            }
            else
            {
                return await source.SumAsync<TResult>(name);
            }
        }

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
        public virtual TResult Avg<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return source.Avg<TResult>(field);
            }
            else
            {
                return source.Avg<TResult>(name);
            }
        }

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> field = null, string name = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetSource();

            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }

            if (!field.IsNull())
            {
                return await source.AvgAsync<TResult>(field);
            }
            else
            {
                return await source.AvgAsync<TResult>(name);
            }
        }

        #endregion

    }

    /// <summary>
    /// SqlSugar 仓储接口
    /// </summary>
    public class SqlSugarRepository<TEntity> : SqlSugarRepository<TEntity, Guid>, IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        public SqlSugarRepository(string connectionStr = null, ConnectionModel connectionModel = null, SqlSugarClient connectionClient = null, IApplicationSession session = null)
            : base(connectionStr, connectionModel, connectionClient, session)
        {

        }

        #region 添加对象/集合

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity">对象</param>
        public override int Insert(TEntity entity)
        {
            if (entity != null && entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            return base.Insert(entity);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override int Insert(List<TEntity> list)
        {
            if (!list.IsEmpty())
            {
                list.ForEach(x =>
                {
                    if (x.Id == Guid.Empty)
                    {
                        x.Id = Guid.NewGuid();
                    }
                });
            }
            return base.Insert(list);
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 添加对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        public override async Task<int> InsertAsync(TEntity entity)
        {
            if (entity != null && entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            return await base.InsertAsync(entity);
        }

        /// <summary>
        /// 添加集合-(可等待)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override async Task<int> InsertAsync(List<TEntity> list)
        {
            if (!list.IsEmpty())
            {
                list.ForEach(x =>
                {
                    if (x.Id == Guid.Empty)
                    {
                        x.Id = Guid.NewGuid();
                    }
                });
            }
            return await base.InsertAsync(list);
        }

        #endregion

        #endregion
    }
}