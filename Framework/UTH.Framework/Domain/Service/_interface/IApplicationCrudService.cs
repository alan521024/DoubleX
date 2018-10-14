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
    /// 增删改查应用服务接口
    /// </summary>
    public interface IApplicationCrudService<TDTOInput> : IApplicationService, IApplicationCrudService<TDTOInput, TDTOInput>
        where TDTOInput : IEntityKeys
    {
    }

    /// <summary>
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<TDTOInput, TEditInput> : IApplicationService, IApplicationCrudService<TDTOInput, TEditInput, TDTOInput>
        where TDTOInput : IEntityKeys
        where TEditInput : IEntityKeys
    {
    }

    /// <summary>
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<TDTOInput, TEditInput, TQueryInput> : IApplicationService, IApplicationCrudService<TDTOInput, TEditInput, TEditInput, TQueryInput, TQueryInput, TQueryInput, Guid>
        where TDTOInput : IEntityKeys
        where TEditInput : IEntityKeys
        where TQueryInput : IEntityKeys
    {
    }

    /// <summary>
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<TDTOInput, in TInsertInput, in TUpdateInput, in TGetInput, in TDeleteInput, TQueryInput, TPrimaryKey> : IApplicationService
        where TDTOInput : IEntityKeys<TPrimaryKey>
        where TUpdateInput : IEntityKeys<TPrimaryKey>
        where TGetInput : IEntityKeys<TPrimaryKey>
        where TDeleteInput : IEntityKeys<TPrimaryKey>
        where TQueryInput : IEntityKeys<TPrimaryKey>
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TDTOInput Get(TGetInput input);

        /// <summary>
        /// 添加对象
        /// </summary>
        TDTOInput Insert(TInsertInput input);

        /// <summary>
        /// 修改对象
        /// </summary>
        TDTOInput Update(TUpdateInput input);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(TDeleteInput input);

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TDTOInput> Query(QueryInput<TQueryInput> query);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagingModel<TDTOInput> Paging(QueryInput<TQueryInput> query);

    }
}
