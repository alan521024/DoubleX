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
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<DTO, TEditInput> : IApplicationService, IApplicationCrudService<DTO, TEditInput, DTO>
        where DTO : IKeys
        where TEditInput : IKeys
    {
    }

    /// <summary>
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<DTO, TEditInput, TQueryInput> : IApplicationService, IApplicationCrudService<DTO, TEditInput, TEditInput, TQueryInput, TQueryInput, TQueryInput>
        where DTO : IKeys
        where TEditInput : IKeys
        where TQueryInput : IKeys
    {
    }

    /// <summary>
    /// 增删改查业务接口
    /// </summary>
    public interface IApplicationCrudService<DTO, in TInsertInput, in TUpdateInput, in TGetInput, in TDeleteInput, TQueryInput> : IApplicationService
        where DTO : IKeys
        where TUpdateInput : IKeys
        where TGetInput : IKeys
        where TDeleteInput : IKeys
        where TQueryInput : IKeys
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DTO Get(TGetInput input);

        /// <summary>
        /// 添加对象
        /// </summary>
        DTO Insert(TInsertInput input);

        /// <summary>
        /// 修改对象
        /// </summary>
        DTO Update(TUpdateInput input);

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
        List<DTO> Query(QueryInput<TQueryInput> query);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagingModel<DTO> Paging(QueryInput<TQueryInput> query);

    }
}
