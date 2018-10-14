namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;


    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IApplicationTree<TRep, TEntity, TId, TInput, TOutput> : IApplicationService, IApplicationDefault<TRep, TEntity, TId, TInput, TOutput>
        where TEntity : class, IEntityTree<TId>
        where TRep : IRepository<TEntity, TId>
    {
    }

    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IApplicationTree<TRep, TEntity, TInput, TOutput> : IApplicationTree<TRep, TEntity, Guid, TInput, TOutput>
    where TEntity : class, IEntityTree<Guid>
    where TRep : IRepository<TEntity, Guid>
    {

    }

    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationTree<TEntity, TInput, TOutput> : IApplicationTree<IRepository<TEntity, Guid>, TEntity, TInput, TOutput>
        where TEntity : class, IEntityTree<Guid>
    {

    }


    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationTree<TRep, TEntity> : IApplicationTree<TRep, TEntity, TEntity, TEntity>
        where TEntity : class, IEntityTree<Guid>
        where TRep : IRepository<TEntity, Guid>
    {

    }

    /// <summary>
    /// 树结构业务操作接口
    /// </summary>
    /// <typeparam name="TRep"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationTree<TEntity> : IApplicationTree<IRepository<TEntity, Guid>, TEntity>
        where TEntity : class, IEntityTree<Guid>
    {

    }

}
